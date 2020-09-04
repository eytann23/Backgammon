using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackgammonWorld.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace BackgammonWorld
{
    public class BackgammonGame
    {
        Board backgammonBoard = new Board();

        int clickedContainer;
        bool isBlackTurn;

        public FlowLayoutPanel GetTriangleContainer(int triangleIndex)
        {
            return backgammonBoard.triangles[triangleIndex].Container;
        }
        public FlowLayoutPanel GetBlackBeatedPlaceContainer()
        {
            return backgammonBoard.BlackBeatedPlace.BeatedCheckersPlace;
        }
        public FlowLayoutPanel GetWhiteBeatedPlaceContainer()
        {
            return backgammonBoard.WhiteBeatedPlace.BeatedCheckersPlace;
        }
        
        const int blackOutsideStock = 0, whiteOutsideStock = 25;
        public bool IsBlackOutsideStockFull()
        {
            if (backgammonBoard.triangles[blackOutsideStock].PiecesAmount == 15)
                return true;
            return false;
        }
        public bool IsWhiteOutsideStockFull()
        {
            if (backgammonBoard.triangles[whiteOutsideStock].PiecesAmount == 15)
                return true;
            return false;
        }

        public void RemovePieceFromTriangle(int sourceTriangleIndex)
        {
            backgammonBoard.triangles[sourceTriangleIndex].Remove();
        }
        public void BeatPiece(int destinationTriangleIndex)
        {
            backgammonBoard.BeatPiece(destinationTriangleIndex);
        }
        public void AddPieceToTriangle(int destinationTriangleIndex)
        {
            backgammonBoard.triangles[destinationTriangleIndex].Add(isBlackTurn);
        }
        public void PassToBlackTurn()
        {
            isBlackTurn = true;
        }
        public void PassToWhiteTurn()
        {
            isBlackTurn = false;
        }
        public void PassTurn()
        {
            isBlackTurn = !isBlackTurn;
        }
        public void SetClickedContainer(int triangleIndex)
        {
            clickedContainer = triangleIndex;
        }
        public void DisableAllBoard()
        {
            backgammonBoard.DisableAllBoard();
        }

        public int GetRandomDiceNumber()
        {
            dicesData.Clear();
            Random number = new Random();
            Thread.Sleep(number.Next(1, 200));
            return number.Next(1, 7);
        }
        List<int> dicesData = new List<int>();
        public void RollDices()
        {
            int firstDice = GetRandomDiceNumber();
            int secondDice = GetRandomDiceNumber();
            if (firstDice == secondDice)
            {
                for (int i = 1; i <= 4; i++)
                    dicesData.Add(firstDice);
            }
            else
            {
                dicesData.Add(firstDice);
                dicesData.Add(secondDice);
            }

            EnableOnlyOptionalSources();
        }
        public List<int> GetDicesData()
        {
            return dicesData;
        }



        void enableOnlyBeatedCheckerAsSource()
        {
            backgammonBoard.DisableAllBoard();

            if (isBlackTurn)
                backgammonBoard.BlackBeatedPlace.BeatedCheckersPlace.Enabled = true;
            else
                backgammonBoard.WhiteBeatedPlace.BeatedCheckersPlace.Enabled = true;
        }
        bool enableDestinationsForBeatedChecker()
        {
            bool isAnyMoveAvaliable = false;
            backgammonBoard.DisableAllBoard();

            if (isBlackTurn)
            {
                for (int i = 19; i <= 24; i++)
                {
                    Triangle currentTriangle = backgammonBoard.triangles[i];
                    if (dicesData.Contains(25 - i))
                        if (currentTriangle.IsTriangleValidAsDestination(isBlackTurn))
                        {
                            isAnyMoveAvaliable = true;
                            currentTriangle.Container.Enabled = true;
                        }
                }
            }
            else
            {
                for (int i = 1; i <= 6; i++)
                {
                    Triangle currentTriangle = backgammonBoard.triangles[i];
                    if (dicesData.Contains(i))
                        if (currentTriangle.IsTriangleValidAsDestination(isBlackTurn))
                        {
                            isAnyMoveAvaliable = true;
                            currentTriangle.Container.Enabled = true;
                        }

                }
            }
            return isAnyMoveAvaliable;

        }
        public bool EnableOnlyOptionalSources()
        {
            backgammonBoard.BlackBeatedPlace.BeatedCheckersPlace.Enabled = false;
            backgammonBoard.WhiteBeatedPlace.BeatedCheckersPlace.Enabled = false;

            if (IsBeatedCheckerWaiting())
            {
                enableOnlyBeatedCheckerAsSource();
                return isBeatedPieceCanComeBackIn();
            }

            backgammonBoard.DisableAllBoard();

            if (areAllTheCheckersAtTheBase())
            {
                enableSourcesForTakingOut();
            }

            bool isAnySourceAvaliable = enableSourcesAccordingToValidDestinations();

            //check if any move is possible
            if (!isAnyCheckerCanBeTakenOut())
                return isAnySourceAvaliable;
            else
                return isAnyCheckerCanBeTakenOut();
        }
        public bool EnableOnlyOptionalDestinations()
        {
            backgammonBoard.triangles[25].Container.Enabled = false;
            backgammonBoard.triangles[0].Container.Enabled = false;
            backgammonBoard.BlackBeatedPlace.BeatedCheckersPlace.Enabled = false;
            backgammonBoard.WhiteBeatedPlace.BeatedCheckersPlace.Enabled = false;
            if (IsBeatedCheckerWaiting())
            {
                return enableDestinationsForBeatedChecker(); ;
            }

            bool isAnyMoveAvaliable = enableDestinationsAccordingToDices();

            if (areAllTheCheckersAtTheBase())
                enableTakingOut();

            if (!isAnyCheckerCanBeTakenOut())
                return isAnyMoveAvaliable;
            else
                return isAnyCheckerCanBeTakenOut();
        }
        bool enableDestinationsAccordingToDices()
        {
            int sourceTriangleIndex = clickedContainer;
            List<int> optionalDestinations = new List<int>();
            foreach (int diceNumber in dicesData)
            {
                int destinationTrianglesIndex = isBlackTurn ? (sourceTriangleIndex - diceNumber) : (sourceTriangleIndex + diceNumber);
                if (destinationTrianglesIndex != 0 && destinationTrianglesIndex != 25)
                    optionalDestinations.Add(destinationTrianglesIndex);
            }

            bool isAnyMoveAvaliable = false;

            foreach (Triangle triangle in backgammonBoard.triangles)
            {
                if (triangle != null)
                {
                    int currentTriangleIndex = triangle.Container.TabIndex;
                    triangle.Container.Enabled = false;

                    if (optionalDestinations.Contains(currentTriangleIndex))
                        if (triangle.IsTriangleValidAsDestination(isBlackTurn))
                        {
                            triangle.Container.Enabled = true;
                            isAnyMoveAvaliable = true;
                        }
                }
            }
            return isAnyMoveAvaliable;
        }
        bool enableSourcesAccordingToValidDestinations()
        {
            bool isAnySourceAvaliable = false;
            for (int i = 1; i <= 24; i++)
            {
                Triangle currentTriangle = backgammonBoard.triangles[i];
                if (currentTriangle != null)
                {
                    if (!currentTriangle.IsTriangleValidAsSource(isBlackTurn))
                        continue;
                    foreach (int diceNumber in dicesData)
                    {
                        int optionalDestinationIndex = isBlackTurn ? (i - diceNumber) : (i + diceNumber);
                        if (optionalDestinationIndex >= 1 && optionalDestinationIndex <= 24)
                        {
                            Triangle optionalDestinationTriangle = backgammonBoard.triangles[optionalDestinationIndex];
                            if (optionalDestinationTriangle.IsTriangleValidAsDestination(isBlackTurn))
                            {
                                currentTriangle.Container.Enabled = true;
                                isAnySourceAvaliable = true;
                            }
                        }
                    }
                }
            }
            return isAnySourceAvaliable;
        }
        void enableTakingOut()
        {

            if (!IsBeatedCheckerWaiting())
                if (areAllTheCheckersAtTheBase())
                    if (checkersThatCanBeTakenOut.Contains(clickedContainer))
                    {
                        if (isBlackTurn)
                            backgammonBoard.triangles[0].Container.Enabled = true;
                        else if (!isBlackTurn)
                            backgammonBoard.triangles[25].Container.Enabled = true;
                    }

        }
        List<int> checkersThatCanBeTakenOut = new List<int>();
        void enableBlackSourcesThatCanBeTakenOut()
        {
            for (int i = 1; i <= 6; i++)
            {
                if (dicesData.Contains(i))
                    if (backgammonBoard.triangles[i].PiecesAmount > 0 && backgammonBoard.triangles[i].IsBlack == isBlackTurn)
                    {
                        checkersThatCanBeTakenOut.Add(i);
                        backgammonBoard.triangles[i].Container.Enabled = true;
                    }
            }
            //if dices bigger than places
            if (areDicesBiggerThanAllCheckersIndexes())
            {
                for (int i = 6; i >= 1; i--)
                    if (backgammonBoard.triangles[i].PiecesAmount > 0 && backgammonBoard.triangles[i].IsBlack == isBlackTurn)
                    {
                        checkersThatCanBeTakenOut.Add(i);
                        backgammonBoard.triangles[i].Container.Enabled = true;
                        break;//only biggest triangles index
                    }
            }
        }
        void enableWhiteSourcesThatCanBeTakenOut()
        {
            for (int i = 19; i <= 24; i++)
            {
                if (dicesData.Contains(25 - i))
                    if (backgammonBoard.triangles[i].PiecesAmount > 0 && backgammonBoard.triangles[i].IsBlack == isBlackTurn)
                    {
                        checkersThatCanBeTakenOut.Add(i);
                        backgammonBoard.triangles[i].Container.Enabled = true;
                    }

            }
            //if dices bigger than places
            if (areDicesBiggerThanAllCheckersIndexes())
            {
                for (int i = 19; i <= 24; i++)
                    if (backgammonBoard.triangles[i].PiecesAmount > 0 && backgammonBoard.triangles[i].IsBlack == isBlackTurn)
                    {
                        checkersThatCanBeTakenOut.Add(i);
                        backgammonBoard.triangles[i].Container.Enabled = true;
                        break;
                    }
            }
        }
        void enableSourcesForTakingOut()
        {
            checkersThatCanBeTakenOut.Clear();
            if (isBlackTurn)
                enableBlackSourcesThatCanBeTakenOut();
            else
                enableWhiteSourcesThatCanBeTakenOut();
        }
        bool isAnyCheckerCanBeTakenOut()
        {

            const int firstTriangleOfBlackBase = 1, lastTriangleOfBlackBase = 6;
            const int firstTriangleOfWhiteBase = 19, lastTriangleOfWhiteBase = 24;
            if (isBlackTurn)
            {
                for (int i = firstTriangleOfBlackBase; i <= lastTriangleOfBlackBase; i++)
                {
                    if (backgammonBoard.triangles[i].Container.Enabled == true || clickedContainer == i)
                        return true;
                }
            }
            else
            {
                for (int i = firstTriangleOfWhiteBase; i <= lastTriangleOfWhiteBase; i++)
                {
                    if (backgammonBoard.triangles[i].Container.Enabled == true || clickedContainer == i)
                        return true;
                }
            }
            return false;
        }
        bool areDicesBiggerThanAllCheckersIndexes()
        {
            int currentTriangleIndex;
            Triangle currentTriangle;
            if (isBlackTurn)
            {
                currentTriangleIndex = 6;
                currentTriangle = backgammonBoard.triangles[currentTriangleIndex];
                if (currentTriangle.PiecesAmount > 0 && currentTriangle.IsBlack==isBlackTurn)
                    return false;
                currentTriangleIndex--;
                while (currentTriangleIndex > 0)
                {
                    currentTriangle = backgammonBoard.triangles[currentTriangleIndex];
                    foreach (int number in dicesData)
                    {
                        if (currentTriangle.PiecesAmount > 0 && currentTriangle.IsBlack == isBlackTurn)
                        {
                            if (!(number > currentTriangleIndex))
                                return false;
                        }      
                    }
                    currentTriangleIndex--;
                }
            }
            else
            {
                currentTriangleIndex = 19;
                currentTriangle = backgammonBoard.triangles[currentTriangleIndex];
                if (currentTriangle.PiecesAmount > 0 && currentTriangle.IsBlack==isBlackTurn)
                    return false;
                currentTriangleIndex++;
                while (currentTriangleIndex < 25)
                {
                    currentTriangle = backgammonBoard.triangles[currentTriangleIndex];
                    foreach (int number in dicesData)
                    {
                        if (currentTriangle.PiecesAmount > 0 && currentTriangle.IsBlack==isBlackTurn)
                            if (!(number > (25 - currentTriangleIndex)))
                                return false;
                    }
                    currentTriangleIndex++;
                }
            }

            return true;
        }
        bool areAllTheCheckersAtTheBase()
        {
            Triangle currentTriangle;
            if (isBlackTurn)
            {
                for (int i = 7; i <= 24; i++)
                {
                    currentTriangle = backgammonBoard.triangles[i];
                    if (currentTriangle.IsBlack==isBlackTurn && currentTriangle.PiecesAmount > 0)
                        return false;
                }
            }
            else
            {
                for (int i = 1; i <= 18; i++)
                {
                    currentTriangle = backgammonBoard.triangles[i];
                    if (currentTriangle.IsBlack == isBlackTurn && currentTriangle.PiecesAmount > 0)
                        return false;
                }
            }

            return true;
        }
        public bool IsBeatedCheckerWaiting()
        {
            if (isBlackTurn && backgammonBoard.BlackBeatedPlace.Amount > 0)
                return true;

            else if ((!isBlackTurn) && backgammonBoard.WhiteBeatedPlace.Amount > 0)
                return true;

            else
                return false;
        }
        bool isBeatedPieceCanComeBackIn()
        {
            if (isBlackTurn)
            {
                for (int i = 19; i <= 24; i++)
                {
                    Triangle currentTriangle = backgammonBoard.triangles[i];
                    if (dicesData.Contains(25 - i))
                        if (currentTriangle.IsTriangleValidAsDestination(isBlackTurn))
                            return true;
                }
            }
            else
            {
                for (int i = 1; i <= 6; i++)
                {
                    Triangle currentTriangle = backgammonBoard.triangles[i];
                    if (dicesData.Contains(i))
                        if (currentTriangle.IsTriangleValidAsDestination(isBlackTurn))
                            return true;
                }
            }
            return false;
        }
        public string IsBlackTurnToString()
        {
            return (isBlackTurn ? "Black" : "White");
        }
        bool isAnyDicesCombinationCanLeadToMove(int sourceIndex)
        {
            Triangle nextTriangle;
            for (int diceNumber = 1; diceNumber <= 6; diceNumber++)
            {
                if (isBlackTurn)
                {
                    if (sourceIndex - diceNumber > 0)
                    {
                        nextTriangle = backgammonBoard.triangles[sourceIndex - diceNumber];
                        if (nextTriangle.IsBlack == isBlackTurn || nextTriangle.PiecesAmount<2)
                            return true;
                    }
                }
                else if (!isBlackTurn)
                {
                    if (sourceIndex + diceNumber < 25)
                    {
                        nextTriangle = backgammonBoard.triangles[sourceIndex + diceNumber];
                        if (nextTriangle.IsBlack == isBlackTurn || nextTriangle.PiecesAmount < 2)
                            return true;
                    }
                }
            }
            return false;
        }
        bool isBeatedPieceHasAnyDestinationOption()
        {
            Triangle optionalDestinationForBeatedPiece;
            if (isBlackTurn)
            {
                for (int triangleIndex = 19; triangleIndex <= 24; triangleIndex++)
                {
                    optionalDestinationForBeatedPiece = backgammonBoard.triangles[triangleIndex];
                    if (optionalDestinationForBeatedPiece.IsBlack == isBlackTurn ||
                        optionalDestinationForBeatedPiece.PiecesAmount < 2)
                        return true;
                }
            }
            else if (!isBlackTurn)
            {
                for (int triangleIndex = 1; triangleIndex <= 6; triangleIndex++)
                {
                    optionalDestinationForBeatedPiece = backgammonBoard.triangles[triangleIndex];
                    if (optionalDestinationForBeatedPiece.IsBlack == isBlackTurn ||
                        optionalDestinationForBeatedPiece.PiecesAmount < 2)
                        return true;
                }
            }
            return false;
        }
        public bool IsAnyMoveOptionAvailableBeforeRollingDices()
        {
            if (IsBeatedCheckerWaiting())
            {
                if (isBeatedPieceHasAnyDestinationOption())
                    return true;
            }
            else if (areAllTheCheckersAtTheBase())
                return true;
            
            for (int triangleIndex=1; triangleIndex <= 24; triangleIndex++)
            {
                Triangle currentTriangle = backgammonBoard.triangles[triangleIndex];
                
                if (currentTriangle.IsBlack==isBlackTurn && currentTriangle.PiecesAmount>0)
                {
                    if (isAnyDicesCombinationCanLeadToMove(triangleIndex))
                        return true;
                }
            }
            return false;
        }
        
        int selectedSourceTriagle, selectedDestinationTriangle;
        public void SetSelectedSourceTriangle(int triangleIndex)
        {
            selectedSourceTriagle = triangleIndex;
        }
        public void SetSelectedDestinationTriangle(int triangleIndex)
        {
            selectedDestinationTriangle = triangleIndex;
        }

        public void RemoveCheckerFromBeatedPlace()
        {
            if (isBlackTurn)
            {
                backgammonBoard.BlackBeatedPlace.Remove();
                dicesData.Remove(25 - selectedDestinationTriangle);
            }
            else
            {
                backgammonBoard.WhiteBeatedPlace.Remove();
                dicesData.Remove(selectedDestinationTriangle);
            }
        }  
        public bool IsBeatingOccurs()
        {
            return ((backgammonBoard.triangles[selectedDestinationTriangle].IsBlack != isBlackTurn) &&
                (backgammonBoard.triangles[selectedDestinationTriangle].PiecesAmount == 1));
        }

    }
}
