# Basic Game Tiles – Stylized Blocks Prototyping Pack

A clean, modular set of stylized tiles designed for rapid prototyping of 3D platformers, puzzles, and grid-based levels in Unity. These assets are optimized for clarity and performance, using a single color palette texture across all models.

---

## 📦 What’s Included

- **Prefabs:** (`../HaniJahanDesign/FreePack/Prefabs`)
  - 13 Platforms – flat, block, ramp/slope, floating, bridge
  - 2 Hazards – spikes ×2
  - 10 Interactables – crate, key, coin, door, 2 buttons, ladder, lever, torch

- **Materials** (`../HaniJahanDesign/FreePack/Materials`):
  - `HJD_BuiltIn_Normal.mat` – Single shared material for Built-in Render Pipeline
  - `HJD_URP_Normal.mat` – Single shared material for Universal Rendering pipeline (URP)

- **Textures** (`../HaniJahanDesign/FreePack/Textures`):
  - `HJD_ColorPal_Normal_01/02.png` – 1024×1024 PNG (~3 KB) shared palette, 2 variations
  - `HJD_Cloud00` – 1024×1024 PNG (decorative cloud with alpha)
  - `HJD_SkyGradient` – 1024×1536 PNG (vertical gradient background)

- **Scenes** (`../HaniJahanDesign/FreePack/Scenes`):
  - `HJD_FP_ContentPreview` – Full prefab overview
  - `HJD_FP_SampleScene_01` – Sample demo scene

- **Editor Tools** (`../HaniJahanDesign/FreePack/Editor`):
  - `MyAssetWelcome.cs` – opening Welcome Window with quick links to docs, support, and updates

- **Reference Image** (root folder):
  - `HJD_BasicTiles_Reference.png` – A sample layout image showcasing the included tiles, for reference.


---

## 🧰 How to Use

1. Open any Unity 2020.3+ project.
2. Drag prefabs from `Assets/HaniJahanDesign/FreePack/Prefabs` into your scene.
3. Apply the correct material based on your render pipeline:
   - **URP** – Use the `HJD_URP_Normal` material.
   - **Built-in RP** – Use the `HJD_BuiltIn_Normal` material.
4. All tiles are pivot-centered and grid-aligned for easy snapping.

---

## 🔧 Technical Highlights

- Models provided in **.fbx** format  
- Pivot-centered, grid-aligned geometry for easy snapping  
- **Ultra low-poly** (28–288 tris; most under 200)  
  - Platforms: 28–52  
  - Slopes/Bridges: 36–168  
  - Hazards: 180–196  
  - Interactables: Coin 116, Crate 188, Torch 261, Key 276, Lever 288  
- UVs: non-overlapping, packed for efficiency  
- Uses a **single shared material and palette texture** (2 variations)  
- Supports **Built-in** + **URP** (materials included)  
- No external dependencies or custom shaders  
- Compatible with **Unity 2020.3 LTS or newer**  
- Designed for **clarity, speed, and early gameplay testing**

---

## 🙋‍♀️ Creator

**Hani Jahan Design**  
Focused on simplicity, modularity, and speed.  
Made to keep your creative process fast and fun. ✨

## 💬 Community & Support
Join the HJD Discord to share feedback, ask questions, and see upcoming tools:
👉 https://discord.gg/7pk5Je9bFT