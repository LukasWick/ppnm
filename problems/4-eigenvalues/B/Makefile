all: plotB1.svg out.B2.txt plotB34.svg
	cat out.B2.txt

plotB1.svg: plotB1.gpi out.plotB1.txt
	gnuplot $<

plotB34.svg: plotB34.gpi out.plotB34.txt
	gnuplot $<

out.plotB1.txt: mainB1.exe
	mono $< > $@

out.B2.txt out.plotB34.txt: mainB2.exe
	mono $< > $@

mainB1.exe: mainB1.cs matlib.dll
	mcs $< -reference:$(lastword $^)

mainB2.exe: mainB2.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs


matlib.dll: ../jac.diag.cs  $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg
