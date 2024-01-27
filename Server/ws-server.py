# -*- coding: utf-8 -*-
import asyncio
import websockets

users = {}
userCount = 1

async def echo(websocket, path):
    global users
    global userCount
    
    async for message in websocket:
        try:
            print('received from client:', message)

            if message.startswith('login?id='):
                id = message[len('login?id='):]
                if not id in users:
                    users[id] = f'Player {userCount}'
                    userCount += 1
                response = f'Hello, {users[id]}'
            
            elif message.startswith('slap?id='):
                id = message[len('slap?id='):]
                response = f'Detect Slap from {users[id]}'

            await websocket.send(response)
        except:
            print('error')

asyncio.get_event_loop().run_until_complete(websockets.serve(echo, '0.0.0.0', 8765))
asyncio.get_event_loop().run_forever()