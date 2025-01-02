#ChatGPT codeimport os
import os
from PIL import Image

img_name = "walk"
# Load the sprite sheet
sprite_sheet_path = f".\Player\{img_name}.png"  # Update this path to your sprite sheet location
sprite_sheet = Image.open(sprite_sheet_path)

# Define the output directory
output_dir = os.path.join(os.path.dirname(sprite_sheet_path), f"{img_name}_frames")  # Save frames in a subdirectory of the sprite sheet's folder

# Create the output folder if it doesn't exist
os.makedirs(output_dir, exist_ok=True)

# Auto-detect frame count based on sprite sheet dimensions
# Assumption: Frames are arranged in a single row, equally spaced
frame_width = sprite_sheet.height  # Assuming frames are square
frame_count = sprite_sheet.width // frame_width

# Create and save individual frames
output_paths = []
for i in range(frame_count):
    # Calculate the bounding box for each frame
    left = i * frame_width
    upper = 0
    right = left + frame_width
    lower = upper + sprite_sheet.height

    # Crop the frame
    frame = sprite_sheet.crop((left, upper, right, lower))
    
    # Save the frame as a new image
    output_path = os.path.join(output_dir, f"{img_name}{i}.png")
    frame.save(output_path)
    output_paths.append(output_path)

# Print results
print(f"Detected {frame_count} frames.")
print("Frames saved at:")
for path in output_paths:
    print(path)
