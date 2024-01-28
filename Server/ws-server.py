# -*- coding: utf-8 -*-
import asyncio
import websockets

users = {}
unityId = 'unity'

async def handleMessage(message, websocket):
    global users
    global unityId
    
    if message.startswith('login?id='):
        id = message[len('login?id='):]
        users[id] = websocket
        response = f'Hello, {id}'
        print(f'Connected: {id}')
    
    elif message.startswith('slap?id='):
        id = message[len('slap?id='):]
        if id != unityId and unityId in users and users[unityId]:
            await users[unityId].send(f'slap?id={id}')
        response = f'Detect Slap from {id}'
        print(f'Slap: {id}')

    return response

async def echo(websocket, path):
    async for message in websocket:
        try:
            #print('received from client:', message)
            response = await handleMessage(message, websocket)
            await websocket.send(response)
        except asyncio.exceptions.IncompleteReadError:
            print('error - websockets.exceptions.ConnectionClosed')
            del users['unity'] # we haven't handle unity socket close event for now
            pass
        except websockets.exceptions.ConnectionClosedError:
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