var inputReader = await File.ReadAllLinesAsync(Path.Join(Directory.GetCurrentDirectory(), "input.txt")); 

var positions = new Dictionary<char, HashSet<(int, int)>>();
var part1Antinodes = new HashSet<(int, int)>();
var part2Antinodes = new HashSet<(int, int)>();
for (var i = 0; i < inputReader.Length; i++)
{
    for (var j = 0; j < inputReader[i].Length; j++)
    {
        if (inputReader[i][j] == '.') continue;
        if (!positions.TryGetValue(inputReader[i][j], out var value))
        {
            positions.Add(inputReader[i][j], [(i, j)]);
        }
        else
        {
            //It implies that we already have at least a pair, so we add both of them as aa antinodes for part two
            part2Antinodes.Add((i, j));
            foreach (var pos in value)
            {
                part2Antinodes.Add(pos);
                var dist1 = (pos.Item1 - i, pos.Item2 - j); //Calculate vector's components between A and B
                var dist2 = (i - pos.Item1, j - pos.Item2); //Calculate vector's components between B and A
                
                //Calculate positions of both antinodes
                var antinode1 = (pos.Item1 + dist1.Item1, pos.Item2 + dist1.Item2);
                var antinode2 = (i + dist2.Item1, j + dist2.Item2);
                
                //Check if this antinode has proper coordinates
                if (antinode1.Item1 >= 0 && antinode1.Item1 < inputReader.Length &&
                    antinode1.Item2 >= 0 && antinode1.Item2 < inputReader[i].Length)
                {
                    part1Antinodes.Add(antinode1);
                    
                    //Calculate coordinates of every antinode in a line (part2)
                    var localI = pos.Item1 + 2 * dist1.Item1;
                    var localJ = pos.Item2 + 2 * dist1.Item2;
                    while (localI >= 0 && localI < inputReader.Length && localJ >= 0 && localJ < inputReader[i].Length)
                    {
                        part2Antinodes.Add((localI, localJ));
                        localI += dist1.Item1;
                        localJ += dist1.Item2;
                    }
                }
                
                //The same thing for second antinode
                if (antinode2.Item1 >= 0 && antinode2.Item1 < inputReader.Length &&
                    antinode2.Item2 >= 0 && antinode2.Item2 < inputReader[i].Length)
                {
                    part1Antinodes.Add(antinode2);
                    var localI = i + 2 * dist2.Item1;
                    var localJ = j + 2 * dist2.Item2;
                    while (localI >= 0 && localI < inputReader.Length && localJ >= 0 && localJ < inputReader[i].Length)
                    {
                        part2Antinodes.Add((localI, localJ));
                        localI += dist2.Item1;
                        localJ += dist2.Item2;
                    }
                }
                
            }
            
            // Add coordinates of checked character to use it later if we find the same one
            value.Add((i, j));
        }
    }
    
}


//Printing answers
Console.WriteLine($"First part: {part1Antinodes.Count}");
Console.WriteLine($"Second part: {part1Antinodes.Union(part2Antinodes).Count()}");

