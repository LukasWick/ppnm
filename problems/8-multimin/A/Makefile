all: OutA.txt
	cat OutA.txt

OutA.txt: mainA.exe
	mono $< >$@

mainA.exe: mainA.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=../qnewton.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) Out* out* *.exe *.dll *.svg *.txt
