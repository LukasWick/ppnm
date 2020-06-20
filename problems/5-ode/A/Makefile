all: PlotA1.svg PlotA2.svg PlotA3.svg OutA.txt
	cat OutA.txt

PlotA1.svg: plotA1.gpi out.plotA1.data
	gnuplot $<

PlotA2.svg: plotA2.gpi out.plotA2.data
	gnuplot $<

PlotA3.svg: plotA3.gpi out.plotA3.data
	gnuplot $<

OutA.txt out.plotA1.data out.plotA2.data out.plotA3.data: mainA.exe
	mono $< >OutA.txt

mainA.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=../ode.integrator.cs
SRC +=../../1-interpolation/C/cspline.cs

matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) Out* *.txt out* *.exe *.dll *.svg *.data
