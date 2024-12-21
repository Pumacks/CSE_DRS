from PIL import Image
import os

# Define the folder containing the images
folder_path = "./Player/Idle"

# Set the target size
target_size = (180, 120)

# Process each image in the folder
for filename in os.listdir(folder_path):
    if filename.lower().endswith(('.png', '.jpg', '.jpeg', '.bmp', '.gif', '.tiff', '.webp')):
        file_path = os.path.join(folder_path, filename)
        
        try:
            with Image.open(file_path) as img:
                # Resize the image
                resized_img = img.resize(target_size, Image.Resampling.LANCZOS)
                # Save the resized image, replacing the original
                resized_img.save(file_path)
                print(f"Resized and replaced: {filename}")
        except Exception as e:
            print(f"Failed to process {filename}: {e}")

 