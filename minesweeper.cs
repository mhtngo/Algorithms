public class Solution {
    //create a jagged array of possible directions
     private int[][] dirs = new int[][]
            {
                    new int[] {0,1},  //right
                    new int[] {0,-1}, //left
                    new int[] {1,0},  //up
                    new int[] {-1,0}, //down
                    new int[] {1,-1}, //up-left (diagonal)
                    new int[] {1,1},  //up-right (diagonal)
                    new int[] {-1,-1},//down-left (diagonal)
                    new int[] {-1,1}  //down-right (diagonal)
            };
    
        public char[][] UpdateBoard(char[][] board, int[] click) {
        
        int row = click[0];      //row index of click input
        int col = click[1];      //column index of click input
        int m = board.Length;    //rows in board = length of jagged array
        int n = board[0].Length; //columns in board = width of an element of jagged array
        
        //if click index is unrevealed mine or a revealed mine:
            //1. change that square to reveal a mine
            //2. return board
        if(board[row][col] == 'M' || board[row][col] == 'X')
        {
            board[row][col] = 'X';
            return board;
        }
            
        int num = 0; //counter to track possible mines in a square's surrounding spaces
            
        //step #1: check surrounding spaces for unrevealed mine ('M')
        foreach(var dir in dirs) //for each direction in jagged array, do following:
        {
            int newRow = dir[0] + row; //check new row 
            int newCol = dir[1] + col; //check new column 
        
            //if new row is valid (within the board) AND
            //if new column is valid (within the board) AND
            //space is an unrevealed mine ('M')
            if(newRow >= 0 && newRow < m &&
               newCol >= 0 && newCol < n &&
               board[newRow][newCol] == 'M')
                    num++; //increment the surrounding mine count
        }
        
        //if any mines surrounding current spot
        if(num >0)
        {
            board[row][col] = (char)(num + '0'); //update spot with mine count (as char)
            return board; //return the updated board 
        }
        
        board[row][col] = 'B'; //update spot with a 'B'
        
        //step #2: now recurse through surrounding spaces if unrevealed space is empty ('E')
        foreach(var dir in dirs)
        {
            int newRow = dir[0] + row; //check new row
            int newCol = dir[1] + col; //check new column
            
            //if new row is valid (within the board) AND
            //if new column is valid (within the board) AND
            //if unrevealed space is empty ('E')
            if(newRow >= 0 && newRow < m &&
               newCol >= 0 && newCol < n &&
               board[newRow][newCol] == 'E')
                    //recursively update board 
                    UpdateBoard(board, new int[]{newRow,newCol});
        }
        
        return board; //return the updated board
    }
}
