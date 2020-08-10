using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidyTurn = ActionMaster.Action.Tidy;
    private ActionMaster.Action resetTurn = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster actionMaster;
    [SetUp]
    public void SetUp() {
        actionMaster = new ActionMaster();
    }
    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnEndTurn()
    {        
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnTidy()
    {
        Assert.AreEqual(tidyTurn, actionMaster.Bowl(8));
    }

    [Test]
    public void T03Bowl28SpareReturnEndTurn()
    {
        actionMaster.Bowl(8);
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T04CheckResetAtStrikeInLastFrame()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(resetTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T05CheckResetAtSpareInLastFrame()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        actionMaster.Bowl(1);
        Assert.AreEqual(resetTurn, actionMaster.Bowl(9));
    }

    [Test]
    public void T06LastRollsEndGame()
    {
        int[] rolls = { 8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 8,2};
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(9));
    }

    [Test]
    public void T07GameEndsAtBowl20()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }
}