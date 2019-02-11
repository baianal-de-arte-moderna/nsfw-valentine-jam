#!/bin/python2
import sys
import csv
import json
import os
import re
import itertools

OPT_MARKER = '[OPT] '
K_ID = 'Id'
K_MESSAGE = 'Text'
K_SPEAKER = 'Speaker'
K_ACTIONS = 'Action'
K_CHARACTERS = 'OnScreen Characters'
K_BGIMAGE = 'Background'
K_BGMUSIC = 'Music'

class Characters:
    @staticmethod
    def from_desc(desc):
        if (desc != 'None'):
            all_chars = dict(itertools.izip_longest(*[iter(re.split(';| - ', desc))] * 2, fillvalue=""))
            return all_chars
        else:
            return {}

class ActionCommandFactory:
    @staticmethod
    def create(action):

        if (action == 'pause'):
            return ActionPauseCommand()
        elif (action.startswith('set ')):
            action.replace('set ', '')
            setvar = action.split(' ')
            return ActionSetVariableCommand(setvar[0], setvar[1])
        elif (action.find('[') != -1):
            return ActionPushVarStoryCommand(action.upper())
        else:
            return ActionPushStoryCommand(action.upper())

        return Command(action, {})

    @staticmethod
    def parse_commands(string):
        commands = []
        actions = string.split(',')
        for action in actions:
            commands.append(ActionCommandFactory.create(action.lower()))
        return commands

class Command:
    def __init__(self, command, parameters):
        self._command = command
        self._parameters = parameters

    def to_dict(self):
        return {
            "command": self._command,
            "parameters": self._parameters
        }

class ChoiceCommand(Command):
    def __init__(self, options):
        Command.__init__(self, "choice", {
            "button0Text": options[0][0],
            "button0StoryName": options[0][1],
            "button1Text": options[1][0],
            "button1StoryName": options[1][1]
        })

class CharactersCommand(Command):
    def __init__(self, characters):
        Command.__init__(self, "characters", characters)

class BgMusicCommand(Command):
    def __init__(self, bgmusic):
        Command.__init__(self, "music", { "snapshotName": bgmusic })

class MessageCommand(Command):
    def __init__(self, message, speaker):
        Command.__init__(self,"message", {
            "name": speaker if speaker != 'None' else '',
            "message": message if message != None else ''
        })
        return

class HidePromptCommand(Command):
    def __init__(self):
        Command.__init__(self, "hide_prompt", {})

class ActionPauseCommand(Command):
    def __init__(self):
        Command.__init__(self, "pause", {})

class ActionSetVariableCommand(Command):
    def __init__(self, var, value):
        Command.__init__(self, "set_variable", {
            "key": var,
            "value": value
        })

class ActionPushStoryCommand(Command):
    def __init__(self, story_name):
        Command.__init__(self, "push_story", {
            "storyName": story_name
        })

class ActionPushVarStoryCommand(Command):
    def __init__(self, var_story_name):
        Command.__init__(self, "push_var_story", {
            "varStoryName": var_story_name
        })

class BackgroundCommand(Command):
    def __init__(self, bgname):
        Command.__init__(self, "background", {
            "imageName": bgname
        })

class Scene:
    def __init__(self):
        self._steps = []
        self._current_characters = None
        self._current_background = None
        self._current_options = None

    def flush_choice(self):
        if (self._current_options != None):
            self._steps.append(ChoiceCommand(self._current_options))

    def push_step(self, step):
        bgmusic = step[K_BGMUSIC]
        bgimage = step[K_BGIMAGE]
        characters = Characters.from_desc(step[K_CHARACTERS])

        if (bgmusic != None and bgmusic != ''):
            self._steps.append(BgMusicCommand(bgmusic))

        if (characters != self._current_characters):
            self._steps.append(CharactersCommand(characters))
            self._current_characters = characters

        if (bgimage != self._current_background):
            self._steps.append(BackgroundCommand(bgimage))
            self._current_background = bgimage

        message = desc[K_MESSAGE]
        action = desc[K_ACTIONS]

        if (message.startswith(OPT_MARKER)):
            if (self._current_options == None):
                self._current_options = []

            message = message.replace(OPT_MARKER, '')
            self._current_options.append([ message, action ])

        else:
            if (message != None and message != ''):
                self.flush_choice()

                self._current_options = None
                speaker = desc[K_SPEAKER]
                self._steps.append(MessageCommand(message, speaker))

            else:
                self._steps.append(HidePromptCommand())

            if (action != None and action != ''):
                action_commands = ActionCommandFactory.parse_commands(action)
                if (action_commands != None and action_commands != []):
                    self._steps.extend(action_commands)

        return

    def dump(self, outstream):
        d = [ cmd.to_dict() for cmd in self._steps ]
        outstream.write(json.dumps(d,indent=2))
        return

class SceneMap:
    def __init__(self, name):
        self._scenes = {}
        self._name = name
        self._current_scene = None
        return

    def parse(self,desc):
        scene_id = desc[K_ID]
        if (scene_id != ''):
            if (self._current_scene != None):
                self._current_scene.flush_choice()
            new_scene = Scene()
            self._scenes[scene_id] = new_scene
            self._current_scene = new_scene
        self._current_scene.push_step(desc)
        return

    def dump(self,prefix):
        for scene_id in self._scenes:
            with open(prefix + '-' + scene_id + '.json', 'w') as outstream:
                self._scenes[scene_id].dump(outstream)
        return

if __name__ == '__main__':
    if len(sys.argv) > 1:
        filename = sys.argv[1]
    else:
        print("Error. Missing filename as first parameter")

    scene_name = os.path.splitext(filename)
    scene_name = os.path.basename(scene_name[0])
    print("Parsing scene: {0}".format(scene_name))
    scene_map = SceneMap(scene_name)
    with open(filename) as scene:
        reader = csv.DictReader([line for line in scene if not line.isspace()], dialect='excel-tab',quoting=csv.QUOTE_NONE)
        for desc in reader:
            #print(desc)
            scene_map.parse(desc)
    out_path = 'out/'
    if (len(sys.argv) == 3):
        out_path = sys.argv[2] + '/'
    scene_map.dump(out_path + scene_name)
