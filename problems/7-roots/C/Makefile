all: plotC.svg
	

plotC.svg: plotC.gpi out.plotC.txt out.plotC.E.txt
	gnuplot $<

out.plotC.txt out.plotC.E.txt: mainC.exe
	mono $< 

mainC.exe: mainC.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=../../2-linear-equations/gr.gs.cs
SRC +=../../1-interpolation/C/cspline.cs
SRC +=../../5-ode/ode.integrator.cs
SRC +=../rootfinder.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg
