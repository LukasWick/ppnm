all: out.txt plotA.svg
	cat out.txt

plotA.svg: plotA.gpi out.plot.txt out.tydata.txt
	gnuplot plotA.gpi
out.txt out.plot.txt out.tydata.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC += ../../2-linear-equations/gr.gs.cs

matlib.dll: least.squares.cs  $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.txt *.exe *.dll *.svg
