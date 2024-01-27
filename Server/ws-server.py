# -*- coding: utf-8 -*-
import asyncio
import websockets

users = {}
userCount = 1

async def handleMessage(message, websocket):
    global users
    global userCount
    
    if message.startswith('login?id='):
        id = message[len('login?id='):]
        if not id in users:
            if id == 'unity':
                users[id] = { 'name': f'Unity', 'socket': websocket }
            else:
                users[id] = { 'name': f'Player {userCount}', 'socket': websocket }
            userCount += 1
        name = users[id]['name']
        response = f'Hello, {name}'
        print(f'Connected: {name}')
    
    elif message.startswith('slap?id='):
        id = message[len('slap?id='):]
        name = users[id]['name']

        if 'unity' in users:
            unitySocket = users['unity']['socket']
            if unitySocket or unitySocket:
                await unitySocket.send(f'slap?name={name}')

        response = f'Detect Slap from {name}'
        print(f'Slap: {name}')

    return response

async def echo(websocket, path):
    async for message in websocket:
        try:
            #print('received from client:', message)
            response = await handleMessage(message, websocket)
            await websocket.send(response)
        except websockets.exceptions.ConnectionClosed:
            print('error - websockets.exceptions.ConnectionClosed')
            del users['unity'] # we haven't handle unity socket close event for now
            pass  
        except:
            print('error')

async def echo_debug(websocket, path):
    async for message in websocket:
        response = await handleMessage(message, websocket)
        await websocket.send(response)

asyncio.get_event_loop().run_until_complete(websockets.serve(echo, '0.0.0.0', 8765))
asyncio.get_event_loop().run_forever()