import cv2
import numpy as np
import os

def project_image(img_path, output_dir):
    # 画像の読み込み
    img = cv2.imread(img_path, cv2.IMREAD_GRAYSCALE)

    # 二値化
    _, binarized = cv2.threshold(img, 128, 255, cv2.THRESH_BINARY_INV)

    # X方向の射影
    x_projection = np.sum(binarized, axis=0)
    x_projection_img = np.zeros_like(img)
    height = binarized.shape[0]
    for i, val in enumerate(x_projection):
        end_y = max(int(height - val // 255), 0)
        cv2.line(x_projection_img, (i, height), (i, end_y), 255, 1)

    # Y方向の射影
    y_projection = np.sum(binarized, axis=1)
    y_projection_img = np.zeros_like(img)
    width = binarized.shape[1]
    for i, val in enumerate(y_projection):
        end_x = min(int(val // 255), width)
        cv2.line(y_projection_img, (0, i), (end_x, i), 255, 1)

    # 結果を保存
    base_name = os.path.basename(img_path).split('.')[0]
    cv2.imwrite(os.path.join(output_dir, f"{base_name}_x_projection.png"), x_projection_img)
    cv2.imwrite(os.path.join(output_dir, f"{base_name}_y_projection.png"), y_projection_img)

if __name__ == "__main__":
    input_dir = "./input_hist/"  # 入力画像が保存されているフォルダのパスを指定
    output_dir = "./output_hist/"  # 結果を保存するフォルダのパスを指定

    if not os.path.exists(output_dir):
        os.makedirs(output_dir)

    for image_name in os.listdir(input_dir):
        if image_name.endswith(('.png', '.jpg', '.jpeg')):
            image_path = os.path.join(input_dir, image_name)
            project_image(image_path, output_dir)
