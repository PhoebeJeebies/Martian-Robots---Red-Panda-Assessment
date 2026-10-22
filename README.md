# Martian Robots

A .NET 10 solution for the **Martian Robots** coding assessment.

## Prerequisites

- [.NET SDK 10.0](https://dotnet.microsoft.com/download)
- Visual Studio 2026 (optional, for IDE workflow)

## Getting Started

1. Clone the repository:

```bash
git clone <your-repo-url>
cd "Martian Robots - Red Panda Assessment"
```

2. Restore dependencies:

```bash
dotnet restore "Martian Robots/Martian Robots.slnx"
```

3. Build the solution:

```bash
dotnet build "Martian Robots/Martian Robots.slnx" --configuration Debug
```

## Run Tests

Run all tests:

```bash
dotnet test "Martian Robots/Martian Robots.slnx" --configuration Debug
```

## Run the Application

```bash
dotnet run --project "Martian Robots/Martian Robots.csproj"
```

## Usage Examples

The app runs in interactive mode and prompts for inputs in this order:

1. Mars upper-right boundary (e.g. `5 3`)
2. Rover start position + orientation (e.g. `1 1 E`)
3. Rover instructions (e.g. `RFRFRFRF`)
4. Repeat steps 2-3 for more rovers
5. Type `done` when finished entering rovers

### Example 1: Basic session

Input sequence:

```text
5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFLFLFL
done
```

Expected output format:

```text
Rover Destinations Output:
1 1 E
3 3 N LOST
2 3 S
```

### Example 2: Invalid Mars input

If Mars boundary input is invalid (for example `1341`), the app prints an error and asks again:

```text
Error: Invalid coordinate format. Please enter two positive numbers separated by a space.
```

### Example 3: Invalid rover instructions

If rover instructions contain invalid characters (anything other than `L`, `R`, `F`), the app prints an error:

```text
Error: Invalid instruction detected. Please enter only L, R, or F as capital letters for the movement instructions.
```

## Input Rules

- Mars boundary must be two integers: `X Y`
- Coordinate bounds: `0` to `50`
- Rover start must be: `X Y D`
  - `D` is one of `N`, `E`, `S`, `W`
- Rover instructions must use only `L`, `R`, `F`

## Project Structure

- `Martian Robots/` - main application code
- `Martian Robots Tests/` - unit tests

## Notes

- Target framework: **.NET 10**
- Use solution file: `Martian Robots/Martian Robots.slnx`

## AI Usage and Assistance Tools
- Github Copilot GPT-5.3-Codex was active in the IDE while coding. 
- Copilot was used for some parsing logic and helped speed up changes as I would update implementations in methods. 
- Copilot was used to help write the ReadMe
- Visual Studio Code CleanUp was active 