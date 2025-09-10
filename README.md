# Revit.AI.Assistant

**Revit.AI.Assistant** is a C#-based Autodesk Revit plugin that integrates AI-powered automation into your BIM workflow.  
The plugin uses the [Ollama](https://ollama.ai/) tool to run the open-source [gpt-oss:20b](https://huggingface.co/mlc-ai/gpt-oss-20b) Large Language Model (LLM) locally.  
The AI generates Python Dynamo scripts based on user prompts, and then the plugin applies these scripts directly to the Revit model using the [`pythonnet`](https://github.com/pythonnet/pythonnet) package.

---

## Features
- **AI-powered assistance** for Revit model automation.
- **Local LLM execution** for privacy and offline capability.
- Generates **Python Dynamo scripts** on demand.
- Executes Python scripts **directly on the Revit model** using `pythonnet`.
- Custom workflow integration without cloud dependencies.

---

## Tech Stack
- **Language:** C#  
- **Platform:** Autodesk Revit API  
- **AI Backend:** [Ollama](https://ollama.ai/)  
- **Model:** [gpt-oss:20b](https://huggingface.co/mlc-ai/gpt-oss-20b)  
- **Python Integration:** [`pythonnet`](https://github.com/pythonnet/pythonnet)  
- **Dynamo Scripting:** Python nodes executed in Revit Dynamo environment  

---

## Dependencies
- [Autodesk Revit API](https://www.autodesk.com/developer-network/platform-technologies/revit)  
- [Ollama](https://ollama.ai/)  
- [gpt-oss:20b model](https://huggingface.co/mlc-ai/gpt-oss-20b)  
- [`pythonnet`](https://github.com/pythonnet/pythonnet)  
- [Dynamo for Revit](https://dynamobim.org/)  

---

## Workflow Overview
1. User provides a **natural language prompt** in the plugin interface.
2. Prompt is sent to the **Ollama runtime** running the `gpt-oss:20b` model locally.
3. LLM generates a **Python Dynamo script** tailored to the request.
4. Plugin uses **`pythonnet`** to execute the script in the Revit Dynamo environment.
5. Changes are applied **directly to the active Revit model**.



