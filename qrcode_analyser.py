import numpy as np 
import pandas as pd
import logging
import warnings
import os
import time
import imageio.v3 as iio
from PIL import Image, ImageOps
import cv2
from flask import Flask , jsonify, request, json, Response
from flask_cors import CORS, cross_origin
import base64
import json
from json import JSONEncoder
from io import BytesIO
import random

app = Flask(__name__)
CORS(app)
logging.basicConfig(filename="qrcode.log", level=logging.DEBUG)
logger = logging.getLogger("testProba")

@app.route('/')
def GetTest():
    
    value = 'Test'
    json_string = json.dumps(value,ensure_ascii = False)
    response = Response(json_string,content_type="application/json; charset=utf-8" )
    return response

@app.route('/DetectQRCode', methods=['post'])
def PostImage():
    
    content_type = request.headers.get('Content-Type')
    data = app.json.loads(request.data)
    imgByteStr = data["image"]
    decode = base64.b64decode(imgByteStr)
    randomInt = random.randint(1, 99999999)
    img = Image.open(BytesIO(decode))
    fileName = "ImgBuffer-" + str(randomInt) + ".jpg"
    img.save(fileName, img.format, quality=100)
    
    value = ""
    if (content_type == 'application/json; charset=utf-8'):
        img = cv2.imread(fileName)
        detect = cv2.QRCodeDetector()
        value, points, straight_qrcode = detect.detectAndDecode(img)
        if value is "":
            im = Image.open(fileName).convert('RGB')
            im_invert = ImageOps.invert(im)
            im_invert.save("Inverted_" + fileName, quality=95);
            img = cv2.imread("Inverted_" + fileName)
            value, points, straight_qrcode = detect.detectAndDecode(img)
            if value is "":
                value = "NOK"
            else:
                os.remove("Inverted_" + fileName)
                os.remove(fileName)
    else:
        return 'Content-Type not supported!'

    json_string = json.dumps(value,ensure_ascii = False)
    response = Response(json_string,content_type="application/json; charset=utf-8" )
    return response


app.run(host='0.0.0.0', port=4201)