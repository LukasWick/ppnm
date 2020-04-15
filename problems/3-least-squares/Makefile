all: plotA.svg outB.txt plotC.svg
	cat outB.txt

plotC.svg: plotC.gpi out.plotC.txt
	gnuplot $<

plotA.svg: plotA.gpi out.plotA.txt
	gnuplot $<

outB.txt out.plotA.txt out.plotC.txt: main.exe
	mono $< 

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC += ../2-linear-equations/gr.gs.cs

matlib.dll: least.squares.cs  $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.txt *.exe *.dll *.svg
