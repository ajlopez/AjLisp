
(define modulus 1000)

(define addlists (lst1 lst2)
	(if (nilp lst1)
		lst2
		(if (nilp lst2)
			lst1
			(cons (plus (first lst1) (first lst2)) (addlist (rest lst1) (rest lst2)))
		)
	)
)

(define multnumberlist (number lst)
	(if (nilp lst)
		nil
		(cons 
			(times number (first lst))
			(multnumberlist number (rest lst))
		)
	)
)

(define multlists (lst1 lst2)
	(if (nilp lst1)
		lst2
		(if (nilp lst2)
			lst1
			(addlists
				(multnumberlist (first lst1) lst2)
				(cons 0 (multlists (rest lst1) lst2))
			)
		)	
)

