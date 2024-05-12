<h1 align="center">Questionnaire</h1>

<p align="center">
Questionnaire program for Object Oriented Programming Evaluatie 4
</p>

## Table of Contents

- [Table of Contents](#table-of-contents)
- [Author](#author)
- [Assignment](#assignment)
- [Questionnaire Library](#questionnaire-library)
  - [UML Diagram](#uml-diagram)
  - [Answer](#answer)
    - [Atributes](#atributes)
    - [Constructor](#constructor)
    - [Methods](#methods)
  - [Question](#question)
    - [Atributes](#atributes-1)
    - [Properties](#properties)
    - [Constructor](#constructor-1)
    - [Methods](#methods-1)
- [Scoreboard Library](#scoreboard-library)
  - [UML Diagram](#uml-diagram-1)
  - [Scoreboard](#scoreboard)
    - [Attributes](#attributes)
    - [Properties](#properties-1)
    - [Constructor](#constructor-2)
    - [Methods](#methods-2)
  - [PlayerScore](#playerscore)
    - [Attributes](#attributes-1)
    - [Constructor](#constructor-3)
    - [Methods](#methods-3)
- [Console Application](#console-application)

## Author

This application was created by [Simon Stijnen](https://github.com/SimonStnn). Student at [Vives University Of Applied Sciences](https://www.vives.be/nl/over-vives/campussen/vives-brugge-xaverianenstraat). Studying [Electronics - ICT](https://www.vives.be/nl/technology/elektronica-ict).

> 25/04/2024 -

## Assignment

This project is the 4th assignment for the Object Oriented Programming course. The assignment details can be found in the [assignment PDF](./Permanente%20evaluatie%204%20-%202024%20-%20versie%20a.pdf).

## Questionnaire Library

### UML Diagram

```mermaid
classDiagram
direction LR
class Answer {
  +Text: string
  +IsCorrect: boolean
  +Answer(text: string, isCorrect: bool)
  +ToString() string
}

class Question {
  -possibleAnswer: List~Answer~
  +<< get/set >>Text: string
  +<< get/set >>ImageUrl: string
  +<< get >>Answers: List~Answer~
  +Question(text: string)
  +Add(answer: Answer)
  +GetAnswer(index: int) Answer
  +ToString() string
}

Answer --o Question
```

### Answer

#### Atributes

- `Text`: The text of the answer
- `IsCorrect`: If the answer is correct or not

#### Constructor

- `Answer(text: string, isCorrect: bool)`: Creates a new answer with the given text and if it is correct or not

#### Methods

- `ToString() string`: Returns the text of the answer

### Question

#### Atributes

- `possibleAnswer`: A list of possible answers

#### Properties

- `Text`: The text of the question
- `ImageUrl`: The url of the image of the question
- `Answers`: The list of possible answers

#### Constructor

- `Question(text: string)`: Creates a new question with the given text

#### Methods

- `Add(answer: Answer)`: Adds a possible answer to the question
- `GetAnswer(index: int): Answer`: Returns the answer at the given index
- `ToString() string`: Returns the text of the question

## Scoreboard Library

### UML Diagram

```mermaid
classDiagram
direction RL
class Scoreboard {
  -scoreList: List~PlayerScore~
  -saveFile: string
  +<< get >>PlayerScores: List~PlayerScore~
  +Scoreboard()
  +Scoreboard(saveFile: string)
  +AddPlayer(player: PlayerScore)
  +AddPlayer(name: string, score: int)
  +SortScoreboard()
  +Clear(clearSaveFile: bool = true)
  -SaveFileExists() bool
  +DeleteSaveFile()
  +Load() List~PlayerScore~
  +Save()
  +ToString() string
}

class PlayerScore {
  -name: string
  -score: int
  +Name: string
  +Score: int
  +PlayerScore(name: string, score: int)
  +ToString() string
}

PlayerScore --o Scoreboard
```

### Scoreboard

#### Attributes

- `scoreList`: A list of `PlayerScore` objects
- `saveFile`: The file to save the scoreboard to

#### Properties

- `PlayerScores`: The list of `PlayerScore` objects

#### Constructor

- `Scoreboard()`: Creates a new scoreboard
- `Scoreboard(saveFile: string)`: Creates a new scoreboard with the given save file

#### Methods

- `SaveFileExists() bool`: Checks if the save file exists
- `AddPlayer(player: PlayerScore)`: Adds a player to the scoreboard
- `AddPlayer(name: string, score: int)`: Adds a player to the scoreboard
- `SortScoreboard()`: Sorts the scoreboard by score
- `Clear(clearSaveFile: bool = true)`: Clears the scoreboard
- `DeleteSaveFile()`: Deletes the save file
- `Load() List~PlayerScore~`: Loads the scoreboard from the save file
- `Save()`: Saves the scoreboard to the save file
- `ToString() string`: Returns the scoreboard as a string

### PlayerScore

#### Attributes

- `name`: The name of the player
- `score`: The score of the player
- `Name`: The name of the player
- `Score`: The score of the player

#### Constructor

- `PlayerScore(name: string, score: int)`: Creates a new player score

#### Methods

- `ToString() string`: Returns the player score as a string

## Console Application

The console application will use the [Questionnaire](#questionnaire-library) and [Scoreboard](#scoreboard-library) libraries to create a simple quiz game. The game will ask the user a series of questions and keep track of the score. At the end of the game, the user's score will be saved to a file and displayed on the scoreboard.
