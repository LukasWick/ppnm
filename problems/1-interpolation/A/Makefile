
all: plotA.svg 

plotA.svg: plotA.gpi out-xydata.txt out-linterp.txt out-integral.txt
	gnuplot $<

out-xydata.txt out-linterp.txt out-integral.txt: main.exe
	mono $< 

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
matlib.dll: linspline.cs $(SRC)
	mcs $^ -target:library -out:$@


clean:
	$(RM) Out* *.txt out* *.txt *.exe *.dll *.svg
