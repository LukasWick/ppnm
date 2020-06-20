all: PlotB.svg



PlotB.svg: plotB.gpi out.plotB.data out.plotB.Multiple.data
	gnuplot $<

out.plotB.data out.plotB.Multiple.data: mainB.exe
	mono $< 

mainB.exe: mainB.cs matlib.dll
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
