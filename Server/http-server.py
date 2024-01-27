# -*- coding: utf-8 -*-
import socket
from flask import Flask, render_template
import segno
import os

# https://stackoverflow.com/questions/166506/finding-local-ip-addresses-using-pythons-stdlib
s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
s.connect(('8.8.8.8', 80))
ip = s.getsockname()[0]
s.close()

port = '13579'

app = Flask(__name__)

@app.route('/', methods=['GET', 'POST'])
def home():
   return render_template('index.html', ip = ip)

if __name__ == '__main__':
    url = f'http://{ip}:{port}/'
    print(f'Connect {url} to open the webpage.')
    
    qrcode_path = 'qrcode.png'
    qrcode = segno.make_qr(url)
    qrcode.save(qrcode_path, scale=10)
    
    os.system(qrcode_path)

    app.run(host = '0.0.0.0', port=port)