import json
import sys

def main():
    with open('D:/MinecraftServer/whitelist.json', 'r', encoding='utf-8') as f:
        output = list(json.load(f))
        data = {
            "uuid": sys.argv[1],
            "name": sys.argv[2]
        }
        output.append(data)
    with open("D:/MinecraftServer/whitelist.json","w") as f:
        data_out = output
        json.dump(data_out, f) 
        print("add done!")
main()
