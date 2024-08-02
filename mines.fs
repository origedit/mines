require random.fs

vocabulary mines
also mines definitions

10 constant bomb

: n   #0. next-arg >number 2drop drop ;
n constant width
n constant height
width height * chars constant length

create field   length chars allot
field length erase

: index   swap width * + ;
: tile   chars field + ;
: tile@   tile c@ ;
: tile!   swap tile c! ;

length random tile constant shown
: "bomb"   $1f4a3 xemit ;
: "empty"   $2b1c xemit ;
: "number"   $30 + xemit $fe0f xemit $20e3 xemit ;
: "tile"   ?dup if "number" else "empty" then ;
: "tile"   dup bomb = if drop "bomb" else "tile" then ;
: "tile"   dup c@ "tile" char+ ;
: "tile"   dup shown = if "tile" else ." ||" "tile" ." ||" then ;
: row   width 0 do "tile" loop cr ;
: show   field height 0 do row loop drop ;

: #bombs   length dup 4 rshift swap 3 rshift random + ;
: bombs   #bombs for length random bomb tile! next ;

create +x
-1 , 0 , 1 , -1 , 1 , -1 , 0 , 1 ,
: +x   cells +x + @ + ;
create +y
-1 , -1 , -1 , 0 , 0 , 1 , 1 , 1 ,
: +y   cells +y + @ + ;

variable count
: valid?   0 width within if 0 height within else drop 0 then ;
: bomb?   index tile@ bomb = ;
: ?count   bomb? if 1 count +! then ;
: +count   over over valid? if ?count else 2drop then ;
: set   index count @ tile! ;
: number   0 count ! 8 0 do over i +y over i +x +count loop set ;
: numbers   height 0 do width 0 do j i j i bomb? 0= if number then loop loop ;

bombs numbers show bye
