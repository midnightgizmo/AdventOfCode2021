// See https://aka.ms/new-console-template for more information
using Day05;




// caculate all points between 2 points
//https://stackoverflow.com/questions/21249739/how-to-calculate-the-points-between-two-given-points-and-given-distance
// https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
PuzzleOneAndTwo puzzle = new PuzzleOneAndTwo();
int AnswerToPuzzleOne = puzzle.SolvePuzzleOne();


Console.WriteLine("Answer to puzzle One");
Console.WriteLine(AnswerToPuzzleOne);

int AnswerToPuzzleTwo = puzzle.SolvePuzzleTwo();

Console.WriteLine("Answer to puzzle Two");
Console.WriteLine(AnswerToPuzzleTwo);


