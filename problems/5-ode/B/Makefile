all: plotB.svg 

plotB.svg: plotB.gpi out.plotB.txt
	gnuplot $<

out.plotB.txt: mainB.exe
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
