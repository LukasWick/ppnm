all: OutA.txt
	cat outA.txt

OutA.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs

matlib.dll: ../gr.gs.cs $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) Out* *.txt out* *.txt *.exe *.dll
