﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for quitting the game.
/// </summary>
public class QuitGame : MonoBehaviour
{
    /// <summary>
    /// Method that exits the application in a safe way.
    /// </summary>
    public void Quit() => Application.Quit();
}