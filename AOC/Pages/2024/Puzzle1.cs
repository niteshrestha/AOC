namespace aoc.Components.Pages._2024;

public class Puzzle1
{
    private readonly List<int> _leftList = [];
    private readonly List<int> _rightList = [];

    public Puzzle1(string puzzleInput)
    {
        var lines = puzzleInput.Split(Environment.NewLine);
        foreach(var line in lines)
        {
            // puzzle input from aoc site has multiple whitespaces in between
            // so split based on whitespace and remove all empty strings
            var values = line.Split(" ").ToList();
            values.RemoveAll(s => s == string.Empty);
            
            _leftList.Add(int.Parse(values[0]));
            _rightList.Add(int.Parse(values[1]));
        }
    }

    // Part 1
    public int GetTotalDistance()
    {
        var totalDistance = 0;

        // just sort the number to later substract to get the distance on each node
        _leftList.Sort();
        _rightList.Sort();

        var length = _leftList.Count;

        // just making sure that I don't hit index out of bound
        if(_leftList.Count != _rightList.Count)
        {
            throw new InvalidOperationException("Length doesn't match");
        }

        // lets loop through each node in both list to get the distance
        // between them and add to get the total distance
        for(int i = 0; i < length; i++){
            totalDistance += Math.Abs(_leftList[i] - _rightList[i]);
        }

        return totalDistance;
    }

    // Part 2
    public int GetSimilarityScore()
    {
        var similarityScore = 0;

        // find the occurence of numbers if the right list
        var rightListOccurence = _rightList.GroupBy(x=>x)
            .Select(n => new { Element = n.Key, Count = n.Count()});

        foreach(var leftListItem in _leftList)
        {
            var occurence = rightListOccurence
                .Where(x => x.Element == leftListItem)
                .Select(x=>x.Count)
                .FirstOrDefault();

            similarityScore += leftListItem * occurence;
        }

        return similarityScore;
    }
}
