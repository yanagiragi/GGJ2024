# -*- coding: utf-8 -*-
import socket
from flask import Flask, render_template

# https://stackoverflow.com/questions/166506/finding-local-ip-addresses-using-pythons-stdlib
s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
s.connect(("8.8.8.8", 80))
ip = s.getsockname()[0]
s.close()
print(ip)

app = Flask(__name__)

@app.route('/', methods=['GET', 'POST'])
def home():
   return render_template('index.html', ip = ip)

if __name__ == "__main__":
    app.run(host = '0.0.0.0', port='13579')