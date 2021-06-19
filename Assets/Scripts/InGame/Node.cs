using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{

  public enum Special
  {
    NONE,
    EXTRA_MOVE,
    GO_BACK,
    INSTA_DEATH,
  }

  public class Node : MonoBehaviour
  {
    public bool isTaken;
    public List<Pawn> pawns;
    public Special hasSpecial;
    public Team nodeTeam;
  }
}