SELECT * FROM reservation JOIN site ON reservation.site_id = site.site_id;

SELECT * FROM site JOIN reservation ON reservation.site_id = site.site_id
                        WHERE campground_id = 1
                        AND '2017-10-02' NOT BETWEEN from_date AND to_date 
                        AND '2017-10-08' NOT BETWEEN from_date AND to_date
                        AND from_date NOT BETWEEN '2017-10-02' AND '2017-10-08'
                        AND to_date NOT BETWEEN '2017-10-02' AND '2017-10-08'