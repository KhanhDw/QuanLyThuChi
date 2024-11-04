CREATE DATABASE quan_ly_thu_chi;
USE  quan_ly_thu_chi;

CREATE TABLE tien_thu (
  id INT IDENTITY(1,1) PRIMARY KEY,
  id_danh_muc_thu INT NOT NULL,
  mo_ta nvarchar(255) not null,
  so_tien decimal NOT NULL,
  ten_tai_khoan varchar(255) NOT NULL,
  ngay_thu DATE NOT NULL
);

CREATE TABLE tien_chi (
  id INT PRIMARY KEY IDENTITY(1,1),
  id_danh_muc_chi INT NOT NULL,	
  mo_ta nvarchar(255) not null,
  so_tien decimal NOT NULL,
  ten_tai_khoan varchar(255) NOT NULL,
  ngay_chi DATE NOT NULL
);

CREATE TABLE danh_muc (
  id INT PRIMARY KEY IDENTITY(1,1),
  nameIcon varchar(255) NOT NULL
);

CREATE TABLE danh_muc_thu (
  id INT PRIMARY KEY IDENTITY(1,1),
  id_danh_muc int NOT NULL,
  ten_danh_muc_do_nguoi_dung_dat nvarchar(255) not null unique,
  ten_tai_khoan VARCHAR(255) NOT NULL
);

CREATE TABLE danh_muc_chi (
  id INT PRIMARY KEY IDENTITY(1,1),
  id_danh_muc int NOT NULL,
  ten_danh_muc_do_nguoi_dung_dat nvarchar(255) not null unique,
  ten_tai_khoan varchar(255) NOT NULL
);



CREATE TABLE tai_khoan (
  id INT PRIMARY KEY IDENTITY(1,1),
  ten_tai_khoan VARCHAR(255) NOT NULL UNIQUE,
  gmail varchar(255) NOT NULL UNIQUE,
  mat_khau varchar(255),
  quyen varchar(2) not null
);

/* RÀNG BUỘC*/
ALTER TABLE tai_khoan ADD CONSTRAINT chk_quyen_TaiKhoan CHECK (quyen IN ('ad', 'us'));
ALTER TABLE tai_khoan ADD CONSTRAINT chk_gmail_TaiKhoan CHECK (gmail LIKE '%@%');
ALTER TABLE tai_khoan ADD CONSTRAINT chk_ten_tai_khoan_TaiKhoan CHECK (LEN(ten_tai_khoan) >= 8);
ALTER TABLE tai_khoan ADD CONSTRAINT chk_mat_khau_TaiKhoan CHECK (
	LEN(mat_khau) >= 6
	AND mat_khau LIKE '%[a-z]%'
    AND mat_khau LIKE '%[A-Z]%'
    AND mat_khau LIKE '%[0-9]%'
    AND mat_khau LIKE '%[@$!%*#?&]%'
  );


/* ĐẶT KHÓA */
/*danh muc tổng - danh mục thu - danh mục chi*/
ALTER TABLE danh_muc_thu ADD CONSTRAINT fk_danh_muc_thu__danh_muc
FOREIGN KEY (id_danh_muc) REFERENCES danh_muc(id);

ALTER TABLE danh_muc_chi add constraint fk_danh_muc_chi__danh_muc
foreign key (id_danh_muc) references danh_muc(id);

/*tiền thu và chi , danh mục thu và chi*/
ALTER TABLE tien_thu add constraint fk_tien_thu__danh_muc_thu
foreign key (id_danh_muc_thu) references danh_muc_thu(id);

ALTER TABLE tien_chi add constraint fk_tien_chi__danh_muc_chi
foreign key (id_danh_muc_chi) references danh_muc_chi(id);


/*tài khoản - (tiền thu, chi, danh mục thu,chi)*/
ALTER TABLE tien_chi add constraint fk_tai_khoan__tien_chi foreign key (ten_tai_khoan) references tai_khoan(ten_tai_khoan);
ALTER TABLE tien_thu add constraint fk_tai_khoan__tien_thu foreign key (ten_tai_khoan) references tai_khoan(ten_tai_khoan);
--ALTER TABLE chi_tiet add constraint fk_tai_khoan__chi_tiet foreign key (ten_tai_khoan) references tai_khoan(ten_tai_khoan);
ALTER TABLE danh_muc_chi add constraint fk_tai_khoan__danh_muc_chi foreign key (ten_tai_khoan) references tai_khoan(ten_tai_khoan);
ALTER TABLE danh_muc_thu add constraint fk_tai_khoan__danh_muc_thu foreign key (ten_tai_khoan) references tai_khoan(ten_tai_khoan);


/* XÓA KHÓA */

 -- xóa khóa danh mục---------------
ALTER TABLE danh_muc_thu DROP CONSTRAINT  fk_danh_muc_thu__danh_muc;
ALTER TABLE danh_muc_chi DROP CONSTRAINT  fk_danh_muc_chi__danh_muc;

-- xóa khóa 
ALTER TABLE tien_chi DROP CONSTRAINT  fk_tien_chi__danh_muc_chi;
ALTER TABLE tien_thu DROP CONSTRAINT  fk_tien_thu__danh_muc_thu;
--ALTER TABLE chi_tiet DROP CONSTRAINT  fk_chi_tiet__tien_thu;
--ALTER TABLE chi_tiet DROP CONSTRAINT  fk_chi_tiet__tien_chi;
ALTER TABLE danh_muc_chi DROP CONSTRAINT  fk_tai_khoan__danh_muc_chi;
ALTER TABLE danh_muc_thu DROP CONSTRAINT  fk_tai_khoan__danh_muc_thu;
ALTER TABLE tien_chi DROP CONSTRAINT  fk_tai_khoan__tien_chi;
ALTER TABLE tien_thu DROP CONSTRAINT  fk_tai_khoan__tien_thu;
--ALTER TABLE chi_tiet DROP CONSTRAINT  fk_tai_khoan__chi_tiet;

--  xóa bảng
DROP TABLE tien_thu;
DROP TABLE tien_chi;
DROP TABLE danh_muc_thu;
DROP TABLE danh_muc_chi;
DROP TABLE tai_khoan;
DROP TABLE danh_muc;

select * FROM tien_thu;
select * FROM tien_chi;
select * FROM danh_muc_thu;
select * FROM danh_muc_chi;
select * FROM tai_khoan;
select * FROM danh_muc;

TRUNCATE TABLE tien_thu;
TRUNCATE TABLE tien_chi;
TRUNCATE TABLE danh_muc_thu;
TRUNCATE TABLE danh_muc_chi;
--TRUNCATE TABLE chi_tiet;
TRUNCATE TABLE tai_khoan;
TRUNCATE TABLE danh_muc;
	

-- tài khoản
INSERT INTO tai_khoan (  ten_tai_khoan ,  gmail ,  mat_khau , quyen ) 
VALUES ( 'noneNone', 'admin@gmail.com', 'aA@156' , 'ad' );
INSERT INTO tai_khoan (   ten_tai_khoan ,  gmail ,  mat_khau , quyen ) 
VALUES ( 'hello678', 'vanA@gmail.com', 'aA@156' , 'us');
INSERT INTO tai_khoan (   ten_tai_khoan ,  gmail ,  mat_khau , quyen ) 
VALUES ( 'hello123', 'vanB@gmail.com', 'aA@156' , 'us');
-- danh mục chi
--INSERT INTO danh_muc_chi ( id_danh_muc, ten_tai_khoan ) VALUES ( 3, 'noneNone');
INSERT INTO danh_muc_chi ( id_danh_muc, ten_danh_muc_do_nguoi_dung_dat , ten_tai_khoan ) VALUES ( 1, N'mua bánh', N'noneNone');
INSERT INTO danh_muc_chi ( id_danh_muc, ten_danh_muc_do_nguoi_dung_dat , ten_tai_khoan ) VALUES ( 1, N'mua bánh', 'hello678');
INSERT INTO danh_muc_chi ( id_danh_muc, ten_danh_muc_do_nguoi_dung_dat , ten_tai_khoan ) VALUES ( 2, N'mua kẹo', 'hello678');
-- danh mục thu
INSERT INTO danh_muc_thu (  id_danh_muc, ten_danh_muc_do_nguoi_dung_dat , ten_tai_khoan )VALUES (  1, 'Lương', 'hello678');
INSERT INTO danh_muc_thu (  id_danh_muc, ten_danh_muc_do_nguoi_dung_dat , ten_tai_khoan ) VALUES (  2, 'Lì xì', 'hello678');
-- tiền thu
INSERT INTO tien_thu (  id_danh_muc_thu, mo_ta, so_tien, ten_tai_khoan, ngay_thu  ) 
VALUES ( 9, N'Tiền lương',22, 'hello678', '2024-04-08');
INSERT INTO tien_thu (  id_danh_muc_thu, mo_ta, so_tien, ten_tai_khoan, ngay_thu ) 
VALUES ( 23, N'shoppe',290669, 'hello678', '2024-04-08');
-- tiền chi
INSERT INTO tien_chi (  id_danh_muc_chi, mo_ta, so_tien, ten_tai_khoan, ngay_chi  )
VALUES ( 15, N'ăn trưa', 20000, 'hello', '2024-04-06');
INSERT INTO tien_chi (  id_danh_muc_chi, mo_ta, so_tien, ten_tai_khoan, ngay_chi  ) 
VALUES ( 11, N'ăn sáng',10000, 'hello678', '2024-04-06');
INSERT INTO tien_chi (  id_danh_muc_chi, mo_ta, so_tien, ten_tai_khoan, ngay_chi  ) 
VALUES ( 9, N'ăn sáng',10000, 'hello678', '2024-04-07');
INSERT INTO tien_chi (  id_danh_muc_chi, mo_ta, so_tien, ten_tai_khoan, ngay_chi  ) 
VALUES ( 6, N'ăn 676y yui',125000, 'hello678', '2024-04-08');
INSERT INTO tien_chi (  id_danh_muc_chi, mo_ta, so_tien, ten_tai_khoan, ngay_chi  ) 
VALUES ( 2, N'Mua bán',9123, 'hello678', '2024-04-08');





-- danh mục
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'address_card.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'apple.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'award.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'baby.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'babycarriage.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'baseball.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'baseballbatball.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'basketball.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'basketshopping.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'bath.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'bed.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'bedpulse.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'bicycle.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'bookopenreader.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'bowlrice.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'briefcase.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'briefcasemedical.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'broom.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'bucket.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'bug.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'building.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'buildingcolumns.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'burger.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'bus.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'bussimple.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'cakecandles.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'camera.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'cameraretro.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'car.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'cartarrowdown.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'cartplus.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'cartshopping.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'cat.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'champagneglasses.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'child.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'childdress.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'children.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'church.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'coins.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'dog.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'dumbbell.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'faucetdrip.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'gasPump.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'gift.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'gifts.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'guitar.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'handholdingdollar.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'hospital.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'house.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'motorcycle.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'mughot.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'paintbrush.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'penruler.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'personbiking.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'personcane.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'phone.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'phonevolume.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'piggybank.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'plane.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'school.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'ship.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'shirt.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'smoking.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'taxi.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'utensils.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'wallet.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'wifi.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'winebottle.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'wineglass.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'wrench.png');
INSERT INTO danh_muc ( nameIcon ) VALUES ( 'question.png');










-- Test code ---
DELETE FROM tai_khoan WHERE id = 1;

UPDATE tai_khoan SET ten_tai_khoan = 'helloVersion2' WHERE ten_tai_khoan = 'hello';

SELECT so_tien FROM tien_chi WHERE ten_tai_khoan=1;


SELECT SUM(so_tien) FROM tien_chi WHERE ten_tai_khoan = 'hello';

SELECT danh_muc.nameIcon  FROM danh_muc_chi INNER JOIN danh_muc  
ON danh_muc_chi.id_danh_muc = danh_muc.id
Where danh_muc_chi.ten_tai_khoan = 'hello678';


SELECT id FROM danh_muc WHERE nameIcon = 'briefcase';


UPDATE danh_muc_chi SET ten_danh_muc_do_nguoi_dung_dat = N'Mua vàng' 
WHERE ten_tai_khoan = N'hello678' and ten_danh_muc_do_nguoi_dung_dat = N'mua bánh';




SELECT 
    combined_data.ten as N'tên người dùng',
    combined_data.mota as N'mô tả'
FROM (
    SELECT B.ten, B.mota
    FROM B
    UNION ALL
    SELECT C.ten, C.mota
    FROM C
) AS combined_data

/*
Trong truy vấn con đầu tiên (SELECT B.ten, B.mota FROM B), chúng ta chọn cột 'ten' và 'mota' từ bảng B.

Trong truy vấn con thứ hai (SELECT C.ten, C.mota FROM C), chúng ta chọn cột 'ten' và 'mota' từ bảng C.

UNION ALL được sử dụng để kết hợp kết quả của hai truy vấn con trên lại với nhau. Điều này bao gồm tất cả các dòng từ cả hai truy vấn, mà không cần loại bỏ bất kỳ dòng trùng lặp nào.

Kết quả của UNION ALL được đặt trong một truy vấn con có tên là combined_data và chúng ta chọn cột 'ten' và 'mota' từ nó.

Cuối cùng, chúng ta chọn cột 'ten' và 'mota' từ combined_data để hiển thị trong kết quả cuối cùng.
*/


select * from tien_chi;

select MAX(id) AS smm, so_tien from tien_chi 
where ten_tai_khoan = 'hello678' and ngay_chi = '2024-04-08'
group by so_tien




SELECT tien_chi.id_danh_muc_chi
FROM tien_chi
JOIN danh_muc_chi ON tien_chi.id_danh_muc_chi = danh_muc_chi.id_danh_muc


select id from danh_muc where nameIcon = N'wrench.png'



select ten_danh_muc_do_nguoi_dung_dat from danh_muc_chi where id_danh_muc = 2  and ten_tai_khoan = 'hello678';



SELECT id_danh_muc_chi, SUM(so_tien) AS tong_so_tien
FROM tien_chi
WHERE ten_tai_khoan = 'hello678' 
  AND MONTH(ngay_chi) = 4 
  AND YEAR(ngay_chi) = 2024 
  AND id_danh_muc_chi = 9
GROUP BY id_danh_muc_chi;



SELECT DAY(ngay_chi) AS ngay_chi, SUM(so_tien) AS tong_so_tien FROM tien_chi
WHERE ten_tai_khoan = 'hello678' 
AND MONTH(ngay_chi) = 4 
AND YEAR(ngay_chi) = 2024 
GROUP BY ngay_chi;


SELECT SUM(so_tien) AS tong_tien FROM tien_chi WHERE MONTH(ngay_chi) = 4
AND YEAR(ngay_chi) = 2024 AND ten_tai_khoan = 'hello678';


--UNION ALL : kết hợp 2 bảng thành 1 bảng duy nhất
--SELECT DISTINCT : lấy giá trị duy nhất và bỏ những giá trị có sự trùng lập


SELECT DISTINCT ngay
FROM (
    SELECT ngay_chi AS ngay
    FROM tien_chi
    WHERE MONTH(ngay_chi) = 4 AND YEAR(ngay_chi) = 2024
    UNION ALL
    SELECT ngay_thu AS ngay
    FROM tien_thu
    WHERE MONTH(ngay_thu) = 4 AND YEAR(ngay_thu) = 2024
) AS ngay_tien



select sum(so_tien) as tong_tien From tien_chi where ten_tai_khoan = N'hello678';



select * from tai_khoan;

UPDATE tai_khoan
SET ten_tai_khoan = N'hello123456', 
gmail = N'vanC@gmail.com', 
mat_khau = N'aA@156111' 
WHERE 
ten_tai_khoan = N'hello123' and
gmail = N'vanB@gmail.com' and 
mat_khau = N'aA@156';


delete from tai_khoan where id = 5;


select * FROM tien_thu;
select * FROM tien_chi;
select * FROM danh_muc_thu;
select * FROM danh_muc_chi;
select * FROM tai_khoan;
select * FROM danh_muc;





select id from danh_muc_chi 
where id_danh_muc = 1 and ten_danh_muc_do_nguoi_dung_dat = N'mua bánh' and ten_tai_khoan=N'noneNone';


INSERT INTO tien_chi ( id_danh_muc_chi, mo_ta, so_tien, ten_tai_khoan, ngay_chi  ) 
VALUES ( 3, N'd',34, N'noneNone', N'2024-04-19');


SELECT t.id, d.id_danh_muc as id_danh_muc_chi, t.mo_ta, t.so_tien, t.ten_tai_khoan, t.ngay_chi
FROM tien_chi t
JOIN danh_muc_chi d ON t.id_danh_muc_chi = d.id where t.ngay_chi = N'2024-04-20' and  t.ten_tai_khoan = N'noneNone'  ;

select * from tien_chi where ngay_chi = N'2024-04-20' and ten_tai_khoan=N'noneNone';
select id_danh_muc from danh_muc_chi where id = 2;

SELECT * from danh_muc_chi WHERE ten_tai_khoan = N'noneNone';




SELECT  d.id_danh_muc, SUM(t.so_tien) AS tong_so_tien  
FROM tien_thu t  
JOIN danh_muc_thu d ON t.id_danh_muc_thu = d.id  
where MONTH(t.ngay_thu) = 4  
AND YEAR(ngay_thu) = 2024 
AND t.ten_tai_khoan = N'noneNone'  
AND t.id_danh_muc_thu = 1 
GROUP BY d.id_danh_muc;			


SELECT id_danh_muc_thu, SUM(so_tien) AS tong_so_tien FROM tien_thu 
WHERE ten_tai_khoan = N'noneNone' 
AND MONTH(ngay_thu) = 4
AND YEAR(ngay_thu) = 2024
AND id_danh_muc_thu = 66
GROUP BY id_danh_muc_thu;


UPDATE tien_thu SET mo_ta = N'cá vàng', so_tien = 10000, ngay_thu = N'2024-04-20', id_danh_muc_thu = 1 
WHERE id = 2 and ten_tai_khoan = N'noneNone';


UPDATE danh_muc_chi SET id_danh_muc=3 , ten_danh_muc_do_nguoi_dung_dat = N'mua đất' 
WHERE ten_tai_khoan = N'noneNone' and ten_danh_muc_do_nguoi_dung_dat = N'mua bánh';


UPDATE tai_khoan
SET 
gmail = N'giakhanh@gmail.com', 
mat_khau = N'aB@1234'
WHERE 
ten_tai_khoan = N'noneNone' and 
gmail = N'admin@gmail.com' and 
mat_khau = N'aA@156';


select * FROM tien_thu;
select * FROM tien_chi;
select * FROM danh_muc_thu;
select * FROM danh_muc_chi;
select * FROM tai_khoan;
select * FROM danh_muc;



SELECT DISTINCT ngay
FROM (
SELECT ngay_chi AS ngay
FROM tien_chi
WHERE MONTH(ngay_chi) = 4 AND YEAR(ngay_chi) = 2024 and ten_tai_khoan = N'micamica'
UNION ALL
SELECT ngay_thu AS ngay
FROM tien_thu
WHERE MONTH(ngay_thu) = 4 AND YEAR(ngay_thu) = 2024 and ten_tai_khoan = N'micamica'
) AS ngay_tien