from PIL import Image
import os

# Set the directory containing the images
directory = "./"

# Loop through all files in the directory

def resize():
    for filename in os.listdir(directory):
        if filename.endswith(".png"):
            filepath = os.path.join(directory, filename)
            with Image.open(filepath) as img:
                # Resize image to 50x50 pixels
                img_resized = img.resize((180, 120))
                # Save the resized image (overwrite the original file or use a new name)
                img_resized.save(filepath)
                print(f"Resized {filename} to 50x50 pixels")


def crop():
    for filename in os.listdir(directory):
        if filename.endswith(".png"):
            filepath = os.path.join(directory, filename)
            with Image.open(filepath) as img:
                # Crop image to the bounding box of non-empty pixels
                cropped_img = img.crop(img.getbbox())
                # Save the cropped image (overwrite or use a new name)
                cropped_img.save(filepath)
                print(f"Cropped {filename} to remove empty pixels")


crop()