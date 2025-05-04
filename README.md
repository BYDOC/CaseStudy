
# Case Study: Unique Code Generation and OCR Text Reconstruction

This repository contains solutions to two practical programming questions, designed as part of a case study.

---

## 🔍 Questions Overview

| Question | Topic                        | Description |
|----------|-----------------------------|-------------|
| 1        | Secure Code Generation       | Generate and validate short, tamper-proof codes using a custom alphabet and cryptographic signature. |
| 2        | OCR Text Line Reconstruction | Reconstruct line-by-line receipt text using OCR output with bounding box coordinates. |

---

## 📁 Project Structure

```
.
├── Question1.Console        # Code generation logic
│   └── CodeGenerator.cs
│
├── Question2.Console        # OCR-based receipt text reconstruction
│   └── OcrParser.cs
│
├── ocr_output.json          # Sample OCR data used in Question 2
├── CaseStudy.sln            # Visual Studio solution file
└── README.md                # This file
```

---

## 🧩 Question 1: Unique Code Generator

### ✅ Requirements
- Codes must be 8 characters long and unique.
- Only use characters from the set: ACDEFGHKLMNPRTXYZ234579.
- Users will use these codes through various channels during a campaign period.
- Code generation must follow a custom algorithm.
- Validation must not rely on storage (no database, file, cache, etc.).
- The codes should be difficult to predict or forge algorithmically.


## ✅ How to Run
1. Open `CaseStudy.sln` in Visual Studio.
2. Set `Question2.Console` as the startup project.
3. Run the project (F5).


## ✅ Details
- Generates 8-character codes using a restricted character set: ACDEFGHKLMNPRTXYZ234579.
- The first 5 characters are a randomly generated payload, using a cryptographically secure RNG (by utilizing RandomNumberGenerator class).
- The last 3 characters form a HMAC-SHA256 signature, derived from the payload and a private secret key.
- The IsValid method verifies code integrity purely through algorithmic means — no storage or lookup required.
- Ensures tamper resistance by binding the signature to the payload.




📄 See [`CodeGenerator.cs`](Question1/Question1.Console/CodeGenerator.cs)

---

## 🧾 Question 2: OCR Text Reconstruction


### ✅ Requirements
- You are not responsible for performing the OCR itself — only for processing the provided JSON.
- Use the coordinate data (boundingPoly) to parse.
- The result should reflect the printed structure of the receipt as closely as possible.

### 🔄 Example Output

```
1 TEŞEKKÜRLER
2 GUNEYDOĞU TEKS. GIDA INS SAN. LTD.STI.
3 ORNEKTEPE MAH.ETIBANK CAD.SARAY APT.
...
```

📄 See [`OcrParser.cs`](Question2/Question2.Console/OcrParser.cs)

---

## ✅ How to Run

1. Open `CaseStudy.sln` in Visual Studio.
2. Set `Question2.Console` as the startup project.
3. Run the project (F5).


