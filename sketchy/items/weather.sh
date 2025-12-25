#!/bin/bash

sketchybar --add item weather right \
    --set weather update_freq=300 \
    icon=🌤 \
    script="$PLUGIN_DIR/weather.sh"
