using System.Collections.Generic;

public class PlayerFactory
{
    public List<Player> CreatePlayers()
    {
        List<Player> players = new List<Player>
        {
            CreatePlayer("Alice"),
            CreatePlayer("Bob"),
            CreatePlayer("Chad"),
            CreatePlayer("Dani")
        };

        return players;
    }

    private Player CreatePlayer(string name)
    {
        // Customize the player creation logic as needed
        return new Player(name);
    }
}