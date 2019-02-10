#!/bin/python2
import sys
import csv
import json
import os

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
            all_chars = desc.split(',')
            print (all_chars)
            return {'Keisuke':'Idle'}
        else:
            return {}

class Command:
    def __init__(self, command, parameters):
        self._command = command;
        self._parameters = parameters;

    def to_dict(self):
        return {
            "command": self._command,
            "parameters": self._parameters
        }

class CharactersCommand(Command):
    def __init__(self, characters):
        Command.__init__(self, "characters", characters)

class BgMusicCommand(Command):
    def __init__(self, bgmusic):
        Command.__init__(self, "music", { "snapshotName": bgmusic })

class OptionCommand(Command):
    pass;

class MessageCommand(Command):
    def __init__(self, message, speaker):
        Command.__init__(self,"message", {
            "name": speaker if speaker != 'None' else '',
            "message": message if message != None else ''
        })
        return;

class Scene:
    def __init__(self):
        self._steps = [];
        self._current_characters = None;
        return;

    def push_step(self, step):
        bgmusic = step[K_BGMUSIC];
        characters = Characters.from_desc(step[K_CHARACTERS]);

        if (bgmusic != None and bgmusic != ''):
            self._steps.append(BgMusicCommand(bgmusic));

        if (characters != self._current_characters):
            self._steps.append(CharactersCommand(characters));
            self._current_characters = characters;

        message = desc[K_MESSAGE]
        speaker = desc[K_SPEAKER]
        self._steps.append(MessageCommand(message, speaker))
        return;

    def dump(self, outstream):
        d = [ cmd.to_dict() for cmd in self._steps ]
        outstream.write(json.dumps(d,indent=2))
        return;

class SceneMap:
    def __init__(self, name):
        self._scenes = {};
        self._name = name;
        return;

    def parse(self,desc):
        scene_id = desc[K_ID]
        if (scene_id != ''):
            new_scene = Scene();
            self._scenes[scene_id] = new_scene;
            self._current_scene = new_scene;
        self._current_scene.push_step(desc);
        return;

    def dump(self,prefix):
        for scene_id in self._scenes:
            with open(prefix + '-' + scene_id + '.json', 'w') as outstream:
                self._scenes[scene_id].dump(outstream)
        return;

if __name__ == '__main__':
    if len(sys.argv) > 1:
        filename = sys.argv[1];
    else:
        print("Error. Missing filename as first parameter");

    scene_name = os.path.splitext(filename);
    scene_name = os.path.basename(scene_name[0]);
    print("Parsing scene: {0}".format(scene_name));
    scene_map = SceneMap(scene_name);
    with open(filename) as scene:
        reader = csv.DictReader(scene, dialect='excel-tab');
        for desc in reader:
            scene_map.parse(desc);
    scene_map.dump('out/' + scene_name)
