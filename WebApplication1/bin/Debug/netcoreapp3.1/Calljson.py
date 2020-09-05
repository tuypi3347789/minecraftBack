import json
data = {'data':'我是一個dictionary資料'}
file = open('D:/server/whitelist.json', mode= 'w, r' ,encoding='utf-8')
json.dump(data, file, ensure_ascii=False)