all: OutB.txt
	cat $<

OutB.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs

matlib.dll: ../gr.gs.cs $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.txt *.exe *.dll
