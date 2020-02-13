import cv2
import numpy as np

def main():
    folder = './data/ETL7_img/A/'
    file = 'ETL7LC_1_000001.png'

    dst = cv2.imread(folder + file)
    #二値化
    dst = cv2.cvtColor(dst, cv2.COLOR_BGR2GRAY)
    th, dst = cv2.threshold(dst,0,255,cv2.THRESH_OTSU)
    #ノイズ除去
    kernel = np.ones((3,3),np.uint8)
    dst = cv2.morphologyEx(dst, cv2.MORPH_OPEN, kernel)
    #白黒反転
    dst = cv2.bitwise_not(dst)
    #リサイズ
    dst = cv2.resize(dst, (64,64))

    cv2.imwrite("result.png",dst)

if __name__ == '__main__':
    main()