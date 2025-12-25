#!/usr/bin/env bash

url="localhost:8080/weatherforecast"
json_response=$(curl $url)

location=$(echo $json_response | jq -r '.name')
temp=$(echo $json_response | jq -r '.temp')

sketchybar --set "$NAME" label="$location $temp"
