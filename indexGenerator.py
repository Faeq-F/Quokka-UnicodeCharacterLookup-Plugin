import unicodedata
import sys
import json
import os
allItems = []
def Generate():
    #for every Unicode character in the range of usable characters
    for i in range(0x110000):
        character = chr(i)
        #name of character (description of what it is)
        name = unicodedata.name(character, "")
        item = {"name": f"{character}", "description": f"{name}"}
        allItems.append(item)
    #convert to JSON
    json_string = json.dumps(allItems)
    #save to file
    jsonDumpPath = os.getcwd()+"\index.json"
    with open(jsonDumpPath, 'w') as file:
       file.write(json_string)

Generate()
