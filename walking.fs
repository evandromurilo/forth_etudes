: MAX-Y 20 ;
: MIN-Y 1 ;
: MAX-X 20 ;
: MIN-X 1 ;

: CAN-MOVE-DOWN ( x y -- bool )
    DUP MAX-Y < ;
: CAN-MOVE-UP ( x y -- bool )
    DUP MIN-Y > ;
: CAN-MOVE-LEFT ( x y -- bool )
    OVER MIN-X > ;
: CAN-MOVE-RIGHT ( x y -- bool )
    OVER MAX-X < ;

: SKIP-LINES 0 DO CR LOOP ;
: SKIP-COLS 0 DO 32 EMIT LOOP ;

: 4DUP ( a b c d -- a b c d a b c d )
    >R >R 2DUP I I' 2SWAP R> R> ;

: MATCH-COORDINATES ( ax ay bx by -- bool )
    ROT = ROT ROT = AND ;

: RENDER-COORDINATE ( fx fy px py cx cy )
    4DUP MATCH-COORDINATES IF 35 EMIT DROP DROP ELSE
         >R >R 2OVER R> R> MATCH-COORDINATES IF 42 EMIT ELSE
	 32 EMIT
    THEN THEN ;

: RENDER-LINE ( fx fy px py ly )
    >R MAX-X MIN-X DO I J RENDER-COORDINATE LOOP R> DROP CR ;

: RENDER ( fx fy px py )
    PAGE MAX-Y MIN-Y DO I RENDER-LINE LOOP ;

: CHECK-WIN ( fx fy px py )
    4DUP MATCH-COORDINATES ;

: GAME-LOOP ( fx fy px py )
    RENDER CHECK-WIN IF ." VOCÊ VENCEU!! " 2DROP 2DROP THEN ;

: INIT-FOOD 5 12 ;
: INIT-PLAYER 1 1 ;
: BEGIN INIT-FOOD INIT-PLAYER RENDER ;

: DOWN CAN-MOVE-DOWN IF 1 + THEN GAME-LOOP ;
: D DOWN ;
: UP CAN-MOVE-UP IF 1 - THEN GAME-LOOP ;
: U UP ;
: LEFT CAN-MOVE-LEFT IF SWAP 1 - SWAP THEN GAME-LOOP ;
: L LEFT ;
: RIGHT CAN-MOVE-RIGHT IF SWAP 1 + SWAP THEN GAME-LOOP ;
: R RIGHT ;

BEGIN
