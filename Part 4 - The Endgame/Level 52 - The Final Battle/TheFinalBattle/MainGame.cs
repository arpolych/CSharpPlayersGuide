using TheFinalBattle;

var gameChar1 = new Character("SKELETON");
var gameChar2 = new Character("SKELETON");

var party1 = new Party();
var party2 = new Party();
party1.AddCharacter(gameChar1);
party2.AddCharacter(gameChar2);

var battle = new Battle(party1, party2);
battle.Run();