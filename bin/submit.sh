#!/bin/bash

mkdir -p Submit

cp -r Assets/ Submit/Assets
cp -r ProjectSettings/ Submit/ProjectSettings
cp -r Builds/* Submit/
cp -r README.md Submit/README.md
cp -r Design.txt Submit/Design.txt


zip -r lisalcao_songya_zelda.zip Submit

hh Submit

