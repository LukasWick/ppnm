all: PlotC.svg 

PlotC.svg: plotC.gpi out.plotC.txt
	gnuplot $<

out.plotC.txt: mainC.exe
	mono $< 

mainC.exe: mainC.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=../ode.integrator.cs
SRC +=../../1-interpolation/C/cspline.cs

matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg
