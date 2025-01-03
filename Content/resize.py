#ChatGPT code
from PIL import Image
import os

# Define the folder containing the images


def resize():
    folder_path = "./Player/Idle"
    target_size = (180, 120)
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

 

def resize2():
    name = "attack_frames"
    input_dir = rf'.\Player\{name}'  # Update this path to your input folder
    
    output_dir = os.path.join(input_dir, name)  # Create a subfolder for processed frames
    # Create the output folder if it doesn't exist
    os.makedirs(output_dir, exist_ok=True)
    # Desired size for all images
    target_size = (85, 85)

    max_width = 0
    max_height = 0

    for filename in os.listdir(input_dir):
        if filename.endswith(".png"):
            file_path = os.path.join(input_dir, filename)

            # Open the image
            image = Image.open(file_path)

            # Remove empty (transparent or solid color) pixels by cropping
            bbox = image.getbbox()  # Get the bounding box of non-transparent pixels
            if bbox:
                image = image.crop(bbox)  # Crop the image to the bounding box

            # Update the maximum width and height
            max_width = max(max_width, image.width)
            max_height = max(max_height, image.height)

    # Final target size
    target_size = (max_width, max_height)
    print(f"Auto-calculated target size: {target_size}")

    # Step 2: Resize and save all images to the calculated size
    for filename in os.listdir(input_dir):
        if filename.endswith(".png"):
            file_path = os.path.join(input_dir, filename)

            # Open the image
            image = Image.open(file_path)

            # Remove empty (transparent or solid color) pixels by cropping
            bbox = image.getbbox()
            if bbox:
                image = image.crop(bbox)

            # Create a new image with a transparent background of the target size
            resized_image = Image.new("RGBA", target_size, (0, 0, 0, 0))
            
            # Paste the cropped image onto the center of the new image
            offset = ((target_size[0] - image.width) // 2, (target_size[1] - image.height) // 2)
            resized_image.paste(image, offset)

            # Save the processed image
            output_path = os.path.join(output_dir, filename)
            resized_image.save(output_path)
    print(f"Processed images saved in: {output_dir}")


resize2()