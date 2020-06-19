all: PlotA.svg OutAandB.txt PlotC.svg
	cat OutAandB.txt

PlotC.svg: plotC.gpi out.plotC.data out.tydata.data
	gnuplot $<

PlotA.svg: plotA.gpi out.plotA.data out.tydata.data
	gnuplot $<

OutAandB.txt out.plotA.data out.plotC.data out.tydata.data: main.exe
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
	$(RM) out* *.txt *.exe *.dll *.svg *.data
