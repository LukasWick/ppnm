all: ms.pdf
ms.pdf: ms.tex plot-eulerSpiral.tex
	pdflatex ms
	pdflatex ms # bibtex run 2 times, and run pdflatx afterwards

plot-eulerSpiral.tex: plot-eulerSpiral.gpi out-eulerSpiral.txt
	gnuplot $<

main.exe: main.cs matlib.dll
	mcs $(word 1,$^) -reference:$(lastword $^)

out-eulerSpiral.txt: main.exe
	mono $< > $@



LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/integration/quado8.cs
matlib.dll:  eulerSpiral.cs $(SRC)
	mcs $^ -target:library -out:$@


clean:
	$(RM) *.log *.aux *.pdf *.dvi out* *.txt *.exe *.eps *.dll plot-*.tex *.out
