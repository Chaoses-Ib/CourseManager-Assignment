﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseManager
{
    /// <summary>
    /// Interaction logic for DatabaseManage.xaml
    /// </summary>
    public partial class DatabaseMaintain : Page
    {
        public DatabaseMaintain()
        {
            InitializeComponent();
        }

        private void btnInsertTestData_Click(object sender, RoutedEventArgs e)
        {
            SQLiteCommand cmd = new(
@"INSERT INTO Class VALUES
('123081','信息与计算科学','数学'),
('123082','信息与计算科学','数学'),
('123091','信息与计算科学','数学'),
('123092','信息与计算科学','数学'),
('192081','信息安全','计算机'),
('193092','网络工程','计算机'),
('193102','网络工程','计算机');

INSERT INTO Student VALUES
(20081003516,'王勇',0,'123081'),
(20081001925,'米保军',0,'123082'),
(20091000230,'王金丹',1,'123091'),
(20091000703,'冉启华',1,'123091'),
(20091000827,'李海水',0,'123091'),
(20091000960,'淦述超',0,'123091'),
(20091001155,'郭继昌',0,'123091'),
(20091001450,'苏安钦',1,'123091'),
(20091001587,'周杰',0,'123091'),
(20091001746,'纪小婷',1,'123091'),
(20091001870,'沈阳',0,'123091'),
(20091001972,'冯梦莲',1,'123091'),
(20091002172,'张永超',0,'123091'),
(20091002295,'马晓辉',0,'123091'),
(20091002428,'姬翔',1,'123091'),
(20091002628,'许再成',0,'123091'),
(20091002668,'杜浩',0,'123091'),
(20091002843,'鲁艺翔',1,'123091'),
(20091002877,'童凯',1,'123091'),
(20091003411,'曾凡林',0,'123091'),
(20091003708,'黄敏君',1,'123091'),
(20091003933,'张聪',0,'123091'),
(20091004284,'张万里',0,'123091'),
(20091000143,'高鑫培',1,'123092'),
(20091000253,'张倩倩',1,'123092'),
(20091000574,'王培培',1,'123092'),
(20091000876,'田昆',0,'123092'),
(20091000951,'王彬彬',0,'123092'),
(20091000998,'靳东',0,'123092'),
(20091001083,'杜鹏',0,'123092'),
(20091001137,'胡志成',0,'123092'),
(20091001595,'周强',0,'123092'),
(20091001712,'吕文娟',1,'123092'),
(20091001897,'夏勇',0,'123092'),
(20091002039,'刘晓雯',1,'123092'),
(20091002335,'张沛东',0,'123092'),
(20091002413,'钱利明',0,'123092'),
(20091002496,'赵远洪',0,'123092'),
(20091002652,'王加东',0,'123092'),
(20091002767,'钟渊',0,'123092'),
(20091002862,'方祥瑞',0,'123092'),
(20091003103,'程福波',0,'123092'),
(20091003786,'赵孟',1,'123092'),
(20091003818,'王苗',1,'123092'),
(20091003930,'张靖阳',0,'123092'),
(20081002789,'黄信武',0,'192081'),
(20091003766,'徐帅',0,'193092'),
(20081003494,'兰方杰',0,'193102');

INSERT INTO Teacher VALUES
(123423,'王帅',0,'计算机'),
(982343,'张雪峰',0,'数学'),
(834546,'刘文才',0,'计算机'),
(435323,'常毓昕',1,'计算机'),
(342355,'曹玲',1,'计算机'),
(346634,'胡瑞雪',1,'计算机'),
(563454,'杨泽文',0,'计算机'),
(452452,'潘赵',0,'计算机'),
(546334,'张少涛',0,'计算机'),
(374421,'张寿涛',0,'马克思主义');

INSERT INTO Course VALUES
(11706200,'马克思主义基本原理',374421,3,0),
(10900500,'大学英语',123423,12,0),
(21919600,'计算科学导论',123423,1.5,0),
(21919400,'计算机高级语言程序设计（C++）',123423,3.5,0),
(21213501,'概率论与数理统计',982343,3.5,0),
(21216501,'离散数学',982343,4.5,0),
(212120281,'高等数学',982343,11.5,0),
(21915900,'数据结构',834546,4,0),
(21906601,'计算机组成原理',834546,4,0),
(21902001,'操作系统原理',435323,3.5,0),
(21921002,'计算机网络',435323,3,0),
(21921400,'数据库系统概论',342355,3,0),
(21920300,'面向对象程序设计（C++）',342355,3,0),
(21903202,'汇编语言程序设计',346634,2.5,0),
(21203800,'计算方法',346634,3,0),
(21921502,'编译原理',346634,2.5,0),
(21904802,'计算机体系结构',346634,2.5,0),
(21915601,'软件工程',563454,3,0),
(21907202,'人工智能',563454,2.5,0),
(21921303,'计算机图形学',452452,2,0),
(21922400,'Linux系统应用与开发',452452,2,1),
(21922500,'Java程序设计',546334,2.5,1),
(21922600,'C#程序设计',546334,2.5,1),
(21922700,'并行计算',546334,2,1),
(21907100,'模式识别',563454,2.5,1);

INSERT INTO CourseTaking VALUES
(20081003516,21922600,80,10,NULL),
(20081003516,21919400,80,10,NULL),
(20081003516,21922700,70,10,NULL),
(20081003516,21203800,67,10,NULL),
(20081003516,21919600,80,10,NULL),
(20081003516,21915900,86,9.5,NULL),
(20081003516,21903202,75,10,NULL),
(20081003516,21907100,96,10,NULL),
(20081003516,21922500,71,9.5,NULL),
(20081003516,21213501,84,10,NULL),
(20081003516,21922400,86,10,NULL),
(20081003516,21921303,86,10,NULL),
(20081003516,21921002,69,10,NULL),
(20081001925,21921400,89,10,NULL),
(20081001925,21921002,67,10,NULL),
(20081001925,21919400,44,10,NULL),
(20081001925,21915900,46,10,NULL),
(20081001925,21922400,68,10,NULL),
(20081001925,21906601,80,10,NULL),
(20081001925,21922600,86,10,NULL),
(20081001925,21919600,77,10,NULL),
(20081001925,21907100,79,10,NULL),
(20081001925,21921303,81,10,NULL),
(20081001925,10900500,82,10,NULL),
(20081001925,21915601,64,9.5,NULL),
(20081001925,21903202,84,10,NULL),
(20081001925,21921502,84,10,NULL),
(20081001925,21213501,87,10,NULL),
(20081001925,212120281,64,10,NULL),
(20081001925,11706200,79,10,NULL),
(20081001925,21216501,64,10,NULL),
(20081001925,21203800,73,10,NULL),
(20091000230,21922700,79,9.5,NULL),
(20091000230,21906601,76,10,NULL),
(20091000230,21203800,90,10,NULL),
(20091000230,21213501,73,10,NULL),
(20091000230,21907202,88,10,NULL),
(20091000230,212120281,78,10,NULL),
(20091000230,21920300,96,10,NULL),
(20091000230,21915601,86,10,NULL),
(20091000230,21922600,77,10,NULL),
(20091000230,21907100,78,10,NULL),
(20091000230,21921400,84,10,NULL),
(20091000230,10900500,64,10,NULL),
(20091000230,21919600,82,9.5,NULL),
(20091000230,21915900,86,10,NULL),
(20091000230,21922400,68,10,NULL),
(20091000230,21216501,89,10,NULL),
(20091000230,21921303,85,10,NULL),
(20091000230,21919400,54,10,NULL),
(20091000230,21922500,61,10,NULL),
(20091000230,21921002,78,10,NULL),
(20091000230,11706200,75,10,NULL),
(20091000230,21903202,66,10,NULL),
(20091000703,21906601,84,10,NULL),
(20091000703,21921502,86,10,NULL),
(20091000703,21902001,71,10,NULL),
(20091000703,21907100,92,10,NULL),
(20091000703,21919400,59,10,NULL),
(20091000703,21921002,75,10,NULL),
(20091000703,21921400,83,10,NULL),
(20091000703,21921303,76,10,NULL),
(20091000703,21915900,62,10,NULL),
(20091000703,21922600,75,10,NULL),
(20091000703,21920300,87,9.5,NULL),
(20091000703,21922500,73,10,NULL),
(20091000703,21904802,93,10,NULL),
(20091000703,10900500,97,10,NULL),
(20091000703,21903202,75,10,NULL),
(20091000703,21216501,74,10,NULL),
(20091000703,21203800,63,10,NULL),
(20091000827,21907100,95,10,NULL),
(20091000827,11706200,73,10,NULL),
(20091000827,21907202,68,10,NULL),
(20091000827,21921303,82,10,NULL),
(20091000827,21922500,67,10,NULL),
(20091000827,21919600,69,10,NULL),
(20091000827,21902001,67,10,NULL),
(20091000827,212120281,75,10,NULL),
(20091000827,21915601,58,10,NULL),
(20091000827,21921002,87,10,NULL),
(20091000827,21922600,71,10,NULL),
(20091000827,21213501,73,10,NULL),
(20091000827,21915900,77,9.5,NULL),
(20091000827,21919400,68,10,NULL),
(20091000827,21216501,74,10,NULL),
(20091000827,21922400,58,10,NULL),
(20091000827,21203800,74,10,NULL),
(20091000827,21903202,80,10,NULL),
(20091000827,21906601,54,10,NULL),
(20091000827,21922700,60,10,NULL),
(20091000827,21921502,62,10,NULL),
(20091000827,21921400,69,10,NULL),
(20091000827,21920300,79,10,NULL),
(20091000827,10900500,84,10,NULL),
(20091000960,212120281,72,10,NULL),
(20091000960,21920300,79,10,NULL),
(20091000960,21216501,86,10,NULL),
(20091000960,21903202,86,9.5,NULL),
(20091000960,21907100,81,10,NULL),
(20091000960,21915601,67,10,NULL),
(20091000960,21921303,72,10,NULL),
(20091000960,21902001,82,10,NULL),
(20091000960,21203800,74,10,NULL),
(20091000960,11706200,85,10,NULL),
(20091000960,21915900,67,10,NULL),
(20091000960,21921002,74,10,NULL),
(20091000960,21922600,83,10,NULL),
(20091000960,21922400,46,10,NULL),
(20091000960,21919400,70,9.5,NULL),
(20091000960,21919600,71,10,NULL),
(20091000960,21921502,90,10,NULL),
(20091000960,21921400,71,10,NULL),
(20091000960,21922500,68,10,NULL),
(20091000960,21213501,76,10,NULL),
(20091000960,21907202,50,10,NULL),
(20091000960,21922700,75,10,NULL),
(20091000960,10900500,85,10,NULL),
(20091000960,21906601,86,10,NULL),
(20091000960,21904802,70,10,NULL),
(20091001155,21904802,72,10,NULL),
(20091001155,21902001,55,10,NULL),
(20091001155,21922400,94,9.5,NULL),
(20091001155,21920300,65,10,NULL),
(20091001155,21915900,79,10,NULL),
(20091001155,21922700,63,10,NULL),
(20091001155,21922500,76,10,NULL),
(20091001155,21921400,83,10,NULL),
(20091001155,21203800,55,10,NULL),
(20091001155,21903202,66,10,NULL),
(20091001155,21919400,78,9.5,NULL),
(20091001155,21216501,67,10,NULL),
(20091001155,21921002,83,10,NULL),
(20091001155,21919600,64,10,NULL),
(20091001155,21922600,63,10,NULL),
(20091001155,10900500,71,10,NULL),
(20091001155,212120281,64,10,NULL),
(20091001155,21213501,77,10,NULL),
(20091001155,21906601,71,10,NULL),
(20091001155,21915601,58,10,NULL),
(20091001450,21921303,77,10,NULL),
(20091001450,21213501,59,10,NULL),
(20091001450,21922500,75,10,NULL),
(20091001450,21906601,61,10,NULL),
(20091001450,21921502,60,10,NULL),
(20091001450,21921002,69,10,NULL),
(20091001450,21907202,71,10,NULL),
(20091001450,21919600,48,9.5,NULL),
(20091001450,21907100,79,10,NULL),
(20091001450,21919400,63,10,NULL),
(20091001450,21915601,85,10,NULL),
(20091001450,212120281,63,10,NULL),
(20091001450,21915900,62,10,NULL),
(20091001450,10900500,91,10,NULL),
(20091001450,21921400,71,10,NULL),
(20091001450,21902001,64,10,NULL),
(20091001450,21922700,88,10,NULL),
(20091001450,21216501,78,10,NULL),
(20091001450,21904802,71,10,NULL),
(20091001587,21906601,77,10,NULL),
(20091001587,21915601,94,10,NULL),
(20091001587,21919400,75,10,NULL),
(20091001587,21907202,58,10,NULL),
(20091001587,21919600,73,9.5,NULL),
(20091001587,21904802,72,10,NULL),
(20091001587,21920300,83,10,NULL),
(20091001587,21922500,60,10,NULL),
(20091001587,21921303,81,10,NULL),
(20091001587,21922700,81,10,NULL),
(20091001587,21922600,76,10,NULL),
(20091001587,212120281,74,10,NULL),
(20091001587,21203800,86,10,NULL),
(20091001587,21213501,79,9.5,NULL),
(20091001587,11706200,87,10,NULL),
(20091001587,21921502,70,10,NULL),
(20091001746,212120281,101,10,NULL),
(20091001746,21906601,77,10,NULL),
(20091001746,21903202,95,9.5,NULL),
(20091001746,21904802,69,10,NULL),
(20091001746,21915900,72,10,NULL),
(20091001746,21907100,81,10,NULL),
(20091001746,21213501,69,10,NULL),
(20091001746,21921303,85,10,NULL),
(20091001746,21902001,84,10,NULL),
(20091001746,21921002,70,10,NULL),
(20091001746,21922700,71,10,NULL),
(20091001746,21203800,93,10,NULL),
(20091001746,21922500,66,10,NULL),
(20091001746,21922600,75,10,NULL),
(20091001870,21904802,82,10,NULL),
(20091001870,21922400,86,10,NULL),
(20091001870,21921002,75,10,NULL),
(20091001870,21902001,83,10,NULL),
(20091001870,21922500,73,10,NULL),
(20091001870,10900500,86,10,NULL),
(20091001870,21919400,90,10,NULL),
(20091001870,21216501,63,10,NULL),
(20091001870,21921400,79,9.5,NULL),
(20091001870,21922600,76,10,NULL),
(20091001870,21921303,56,10,NULL),
(20091001870,11706200,87,10,NULL),
(20091001870,21920300,72,10,NULL),
(20091001972,21922400,82,10,NULL),
(20091001972,21915900,73,10,NULL),
(20091001972,21919400,80,9,NULL),
(20091001972,21920300,67,10,NULL),
(20091001972,21216501,55,10,NULL),
(20091001972,21922600,91,10,NULL),
(20091001972,21213501,85,10,NULL),
(20091001972,21904802,97,10,NULL),
(20091001972,21922500,55,10,NULL),
(20091002172,21915900,90,10,NULL),
(20091002172,21915601,75,10,NULL),
(20091002172,21904802,78,10,NULL),
(20091002172,21920300,74,10,NULL),
(20091002172,21907202,62,10,NULL),
(20091002172,21921502,70,10,NULL),
(20091002172,21919400,83,10,NULL),
(20091002172,21919600,62,10,NULL),
(20091002172,21907100,82,10,NULL),
(20091002172,21213501,61,10,NULL),
(20091002172,11706200,68,9.5,NULL),
(20091002172,21903202,70,10,NULL),
(20091002172,21922600,70,10,NULL),
(20091002172,21921303,94,10,NULL),
(20091002172,212120281,60,10,NULL),
(20091002172,21902001,92,10,NULL),
(20091002172,21921400,73,10,NULL),
(20091002295,21915601,88,10,NULL),
(20091002295,21216501,76,9.5,NULL),
(20091002295,21921400,82,10,NULL),
(20091002295,11706200,83,10,NULL),
(20091002295,21921303,69,10,NULL),
(20091002295,21904802,63,10,NULL),
(20091002295,21919600,76,10,NULL),
(20091002295,21920300,83,10,NULL),
(20091002295,21907100,66,10,NULL),
(20091002428,21922500,72,10,NULL),
(20091002428,21921303,54,10,NULL),
(20091002428,21216501,78,10,NULL),
(20091002428,21203800,87,10,NULL),
(20091002428,11706200,89,10,NULL),
(20091002428,21921002,75,9.5,NULL),
(20091002428,21915900,88,10,NULL),
(20091002428,21919400,84,10,NULL),
(20091002428,212120281,84,9.5,NULL),
(20091002428,21920300,54,10,NULL),
(20091002428,21919600,78,10,NULL),
(20091002428,21915601,86,10,NULL),
(20091002428,21921502,66,10,NULL),
(20091002428,21922400,78,10,NULL),
(20091002428,21907202,68,10,NULL),
(20091002428,21904802,90,10,NULL),
(20091002428,10900500,84,10,NULL),
(20091002428,21213501,62,10,NULL),
(20091002428,21921400,51,10,NULL),
(20091002628,10900500,47,10,NULL),
(20091002628,21919600,67,10,NULL),
(20091002628,21203800,75,10,NULL),
(20091002628,21915900,64,10,NULL),
(20091002668,21922500,66,10,NULL),
(20091002668,21920300,92,10,NULL),
(20091002668,21922700,72,10,NULL),
(20091002668,21921502,56,9.5,NULL),
(20091002668,21907100,70,10,NULL),
(20091002668,21213501,76,10,NULL),
(20091002843,21920300,90,10,NULL),
(20091002843,21922600,68,10,NULL),
(20091002843,21922700,73,10,NULL),
(20091002843,21922500,60,10,NULL),
(20091002843,212120281,63,10,NULL),
(20091002843,21915601,76,10,NULL),
(20091002843,21213501,65,10,NULL),
(20091002843,21921002,87,10,NULL),
(20091002843,21921400,82,10,NULL),
(20091002843,21919400,59,10,NULL),
(20091002843,21904802,61,10,NULL),
(20091002843,21907100,76,9.5,NULL),
(20091002843,21203800,83,10,NULL),
(20091002843,21216501,83,10,NULL),
(20091002843,21902001,63,10,NULL),
(20091002843,11706200,67,10,NULL),
(20091002843,21903202,70,10,NULL),
(20091002843,21922400,81,10,NULL),
(20091002843,21915900,73,10,NULL),
(20091002843,21919600,76,10,NULL),
(20091002843,21906601,58,10,NULL),
(20091002843,21921303,82,10,NULL),
(20091002843,21921502,76,9.5,NULL),
(20091002877,21903202,82,10,NULL),
(20091002877,21921002,81,10,NULL),
(20091002877,21904802,91,10,NULL),
(20091002877,21915601,81,9.5,NULL),
(20091002877,21907202,62,10,NULL),
(20091002877,21203800,71,10,NULL),
(20091003411,21919400,67,10,NULL),
(20091003411,21907100,87,9.9,NULL),
(20091003411,21903202,87,10,NULL),
(20091003708,21906601,80,10,NULL),
(20091003708,21904802,74,10,NULL),
(20091003708,21921002,88,10,NULL),
(20091003708,21922500,84,9.5,NULL),
(20091003708,21922400,78,10,NULL),
(20091003708,21216501,75,10,NULL),
(20091003708,21203800,87,10,NULL),
(20091003708,21903202,82,10,NULL),
(20091003708,21920300,61,10,NULL),
(20091003708,21922600,76,10,NULL),
(20091003708,21919400,66,10,NULL),
(20091003708,212120281,88,10,NULL),
(20091003708,10900500,60,10,NULL),
(20091003708,21907100,76,10,NULL),
(20091003708,21915601,77,9.5,NULL),
(20091003933,21921400,71,10,NULL),
(20091003933,21915900,66,10,NULL),
(20091003933,21922500,87,10,NULL),
(20091003933,21203800,64,10,NULL),
(20091003933,21919600,78,10,NULL),
(20091003933,21216501,84,10,NULL),
(20091003933,21903202,92,10,NULL),
(20091003933,21921002,76,10,NULL),
(20091003933,21921303,77,10,NULL),
(20091003933,21906601,71,10,NULL),
(20091003933,21922400,70,10,NULL),
(20091003933,21907100,69,10,NULL),
(20091003933,21922600,56,10,NULL),
(20091003933,21907202,78,10,NULL),
(20091003933,212120281,82,10,NULL),
(20091003933,11706200,68,9.5,NULL),
(20091003933,21922700,62,10,NULL),
(20091003933,21904802,81,10,NULL),
(20091003933,21915601,82,10,NULL),
(20091003933,10900500,53,10,NULL),
(20091003933,21919400,86,10,NULL),
(20091003933,21920300,76,10,NULL),
(20091004284,21921400,78,10,NULL),
(20091004284,11706200,82,10,NULL),
(20091004284,21906601,61,9.9,NULL),
(20091004284,21920300,69,10,NULL),
(20091000143,21920300,70,9.5,NULL),
(20091000143,21921303,81,10,NULL),
(20091000143,21921002,102,10,NULL),
(20091000143,21922600,77,10,NULL),
(20091000143,21921502,63,10,NULL),
(20091000143,21922700,64,10,NULL),
(20091000143,11706200,79,9.5,NULL),
(20091000143,21904802,63,10,NULL),
(20091000143,21921400,92,10,NULL),
(20091000143,21919600,90,10,NULL),
(20091000143,21907100,71,10,NULL),
(20091000143,21216501,55,10,NULL),
(20091000143,21915900,78,10,NULL),
(20091000143,21915601,76,10,NULL),
(20091000143,21922500,89,10,NULL),
(20091000143,21213501,70,10,NULL),
(20091000143,21922400,77,10,NULL),
(20091000143,21907202,73,10,NULL),
(20091000143,21906601,73,10,NULL),
(20091000143,212120281,68,10,NULL),
(20091000253,21920300,70,10,NULL),
(20091000253,21213501,61,10,NULL),
(20091000253,21903202,92,10,NULL),
(20091000253,21919400,49,10,NULL),
(20091000253,10900500,87,9.5,NULL),
(20091000253,21922600,71,10,NULL),
(20091000253,21921502,74,10,NULL),
(20091000253,21922700,68,10,NULL),
(20091000253,21915601,83,10,NULL),
(20091000253,21203800,62,10,NULL),
(20091000253,21922400,74,10,NULL),
(20091000253,21922500,58,10,NULL),
(20091000574,21907202,80,9.5,NULL),
(20091000574,21922400,62,10,NULL),
(20091000574,21903202,79,10,NULL),
(20091000574,21921002,75,10,NULL),
(20091000574,21203800,68,10,NULL),
(20091000574,21920300,78,10,NULL),
(20091000574,21919400,85,10,NULL),
(20091000574,21922700,70,10,NULL),
(20091000574,11706200,74,10,NULL),
(20091000574,212120281,98,9.5,NULL),
(20091000574,21921303,71,10,NULL),
(20091000574,21921502,73,10,NULL),
(20091000574,21921400,69,10,NULL),
(20091000574,21919600,73,10,NULL),
(20091000574,21907100,97,10,NULL),
(20091000574,21915900,66,10,NULL),
(20091000574,10900500,81,10,NULL),
(20091000574,21904802,63,10,NULL),
(20091000574,21906601,72,9.5,NULL),
(20091000574,21213501,65,10,NULL),
(20091000876,21921502,80,10,NULL),
(20091000876,212120281,79,10,NULL),
(20091000876,21903202,65,10,NULL),
(20091000876,21907202,63,10,NULL),
(20091000876,21921400,79,10,NULL),
(20091000876,21920300,55,9.5,NULL),
(20091000876,21902001,85,10,NULL),
(20091000876,21919600,67,10,NULL),
(20091000876,21203800,79,10,NULL),
(20091000876,21922600,68,10,NULL),
(20091000876,21922700,89,9.5,NULL),
(20091000876,21213501,73,10,NULL),
(20091000876,21919400,68,10,NULL),
(20091000876,21904802,74,10,NULL),
(20091000876,21921303,82,10,NULL),
(20091000876,21906601,73,10,NULL),
(20091000876,10900500,89,10,NULL),
(20091000876,21907100,91,10,NULL),
(20091000876,11706200,75,10,NULL),
(20091000951,21907202,64,10,NULL),
(20091000998,11706200,91,10,NULL),
(20091000998,21922700,68,10,NULL),
(20091000998,21915900,73,10,NULL),
(20091000998,21906601,52,10,NULL),
(20091000998,21902001,68,10,NULL),
(20091000998,21903202,80,10,NULL),
(20091000998,21915601,79,10,NULL),
(20091000998,10900500,70,10,NULL),
(20091000998,21922500,86,9,NULL),
(20091000998,21203800,71,10,NULL),
(20091000998,21922400,62,10,NULL),
(20091000998,212120281,84,10,NULL),
(20091000998,21921400,71,9.5,NULL),
(20091000998,21919600,101,10,NULL),
(20091000998,21213501,92,10,NULL),
(20091000998,21907202,64,10,NULL),
(20091000998,21921002,58,10,NULL),
(20091000998,21907100,69,10,NULL),
(20091000998,21919400,65,10,NULL),
(20091000998,21922600,65,10,NULL),
(20091000998,21921502,80,10,NULL),
(20091000998,21904802,54,10,NULL),
(20091000998,21921303,71,10,NULL),
(20091000998,21216501,73,10,NULL),
(20091001083,212120281,83,10,NULL),
(20091001083,21919400,73,10,NULL),
(20091001083,21919600,72,10,NULL),
(20091001083,21922700,69,10,NULL),
(20091001083,21906601,76,10,NULL),
(20091001083,21921502,64,10,NULL),
(20091001083,21921002,64,10,NULL),
(20091001083,21902001,68,10,NULL),
(20091001083,21922400,74,9.5,NULL),
(20091001083,21921400,75,10,NULL),
(20091001083,21213501,81,10,NULL),
(20091001083,11706200,90,10,NULL),
(20091001083,21203800,78,10,NULL),
(20091001083,21922600,98,10,NULL),
(20091001083,21907100,75,10,NULL),
(20091001083,21907202,70,10,NULL),
(20091001083,21904802,77,10,NULL),
(20091001083,21903202,80,10,NULL),
(20091001083,21922500,96,10,NULL),
(20091001083,21921303,67,10,NULL),
(20091001083,10900500,71,10,NULL),
(20091001083,21920300,85,10,NULL),
(20091001083,21915900,80,10,NULL),
(20091001137,21915900,85,10,NULL),
(20091001137,21907202,80,10,NULL),
(20091001137,11706200,58,9.5,NULL),
(20091001137,21919600,85,10,NULL),
(20091001595,21904802,89,10,NULL),
(20091001595,21216501,68,10,NULL),
(20091001595,21922700,73,10,NULL),
(20091001595,21203800,73,10,NULL),
(20091001595,21902001,70,10,NULL),
(20091001595,21921303,78,10,NULL),
(20091001595,21921400,84,10,NULL),
(20091001595,21922500,96,10,NULL),
(20091001595,21915900,74,10,NULL),
(20091001595,21213501,90,10,NULL),
(20091001595,21919400,84,10,NULL),
(20091001595,11706200,74,10,NULL),
(20091001712,21907202,88,10,NULL),
(20091001712,10900500,86,10,NULL),
(20091001712,21919600,81,10,NULL),
(20091001712,21919400,72,10,NULL),
(20091001712,21922400,84,10,NULL),
(20091001712,21922700,79,10,NULL),
(20091001712,21921502,67,10,NULL),
(20091001712,21216501,81,10,NULL),
(20091001712,21915601,80,10,NULL),
(20091001712,21213501,76,10,NULL),
(20091001712,212120281,80,10,NULL),
(20091001897,21907100,78,10,NULL),
(20091001897,21921002,71,10,NULL),
(20091001897,21922400,63,10,NULL),
(20091001897,21919600,76,10,NULL),
(20091001897,21904802,83,9.5,NULL),
(20091001897,10900500,64,10,NULL),
(20091001897,21915601,67,10,NULL),
(20091001897,21216501,93,10,NULL),
(20091001897,21921303,85,10,NULL),
(20091001897,21919400,74,10,NULL),
(20091001897,21921400,87,10,NULL),
(20091001897,212120281,64,10,NULL),
(20091001897,21213501,71,10,NULL),
(20091001897,21203800,68,10,NULL),
(20091001897,21922700,65,10,NULL),
(20091002039,21903202,72,10,NULL),
(20091002039,21919600,76,10,NULL),
(20091002039,21921002,56,9.5,NULL),
(20091002039,21919400,67,10,NULL),
(20091002039,21213501,70,10,NULL),
(20091002039,21922400,76,10,NULL),
(20091002039,21906601,77,10,NULL),
(20091002039,21904802,66,10,NULL),
(20091002039,21907100,75,10,NULL),
(20091002039,21921400,82,10,NULL),
(20091002039,21907202,69,10,NULL),
(20091002335,21907100,64,9,NULL),
(20091002335,21922400,82,10,NULL),
(20091002335,21922500,83,10,NULL),
(20091002335,21919600,100,10,NULL),
(20091002335,21903202,56,10,NULL),
(20091002335,21907202,65,10,NULL),
(20091002335,21922700,73,10,NULL),
(20091002335,21920300,83,9.5,NULL),
(20091002335,21921502,60,10,NULL),
(20091002335,21904802,64,10,NULL),
(20091002335,21921002,75,10,NULL),
(20091002335,21906601,65,10,NULL),
(20091002335,21203800,71,10,NULL),
(20091002335,21915601,64,10,NULL),
(20091002335,212120281,77,10,NULL),
(20091002335,21915900,61,10,NULL),
(20091002335,21922600,73,10,NULL),
(20091002335,11706200,71,10,NULL),
(20091002335,10900500,68,10,NULL),
(20091002335,21216501,62,10,NULL),
(20091002335,21919400,63,10,NULL),
(20091002413,212120281,77,9.5,NULL),
(20091002413,21922600,63,10,NULL),
(20091002413,21915601,85,10,NULL),
(20091002413,21903202,54,10,NULL),
(20091002413,21919400,80,10,NULL),
(20091002413,21216501,89,10,NULL),
(20091002413,10900500,87,10,NULL),
(20091002413,21922700,59,9,NULL),
(20091002413,21907202,86,10,NULL),
(20091002413,21920300,83,10,NULL),
(20091002413,21906601,64,10,NULL),
(20091002413,21213501,64,10,NULL),
(20091002413,21904802,55,10,NULL),
(20091002413,21907100,78,10,NULL),
(20091002413,21919600,77,10,NULL),
(20091002413,21902001,87,10,NULL),
(20091002413,21921400,92,10,NULL),
(20091002413,11706200,86,10,NULL),
(20091002413,21921303,70,10,NULL),
(20091002413,21203800,73,10,NULL),
(20091002413,21922500,89,10,NULL),
(20091002413,21921002,72,10,NULL),
(20091002413,21921502,93,9.5,NULL),
(20091002496,21907100,64,10,NULL),
(20091002496,21920300,94,10,NULL),
(20091002496,21922600,71,10,NULL),
(20091002496,21903202,70,10,NULL),
(20091002496,21902001,54,10,NULL),
(20091002496,21922700,62,10,NULL),
(20091002496,21919600,69,9.5,NULL),
(20091002496,21922500,82,10,NULL),
(20091002496,21213501,80,10,NULL),
(20091002496,21203800,65,10,NULL),
(20091002496,21921400,69,9,NULL),
(20091002496,21915601,65,10,NULL),
(20091002496,21915900,79,10,NULL),
(20091002496,11706200,76,10,NULL),
(20091002496,21921002,70,10,NULL),
(20091002496,21919400,75,10,NULL),
(20091002496,21921502,65,10,NULL),
(20091002496,21904802,77,10,NULL),
(20091002496,21907202,69,10,NULL),
(20091002652,21922700,61,10,NULL),
(20091002652,21920300,106,10,NULL),
(20091002652,21904802,67,10,NULL),
(20091002652,11706200,71,10,NULL),
(20091002652,21921303,54,10,NULL),
(20091002652,21915601,81,10,NULL),
(20091002652,21919400,59,10,NULL),
(20091002767,212120281,67,10,NULL),
(20091002767,21203800,90,10,NULL),
(20091002767,10900500,78,9.5,NULL),
(20091002767,11706200,88,10,NULL),
(20091002767,21213501,63,10,NULL),
(20091002767,21922600,82,10,NULL),
(20091002767,21921303,84,10,NULL),
(20091002767,21921502,89,10,NULL),
(20091002767,21907100,88,10,NULL),
(20091002767,21922500,73,10,NULL),
(20091002767,21915900,84,10,NULL),
(20091002767,21922400,71,10,NULL),
(20091002767,21921002,85,10,NULL),
(20091002767,21920300,85,10,NULL),
(20091002767,21906601,69,10,NULL),
(20091002862,21922400,62,10,NULL),
(20091002862,21907202,83,10,NULL),
(20091002862,21922500,91,9.5,NULL),
(20091002862,21921400,86,10,NULL),
(20091002862,21203800,61,10,NULL),
(20091002862,21907100,56,10,NULL),
(20091002862,21906601,74,10,NULL),
(20091002862,10900500,70,10,NULL),
(20091002862,21921303,92,10,NULL),
(20091002862,21903202,66,10,NULL),
(20091002862,212120281,80,10,NULL),
(20091002862,21922700,91,10,NULL),
(20091002862,21919400,65,10,NULL),
(20091003103,21922600,54,10,NULL),
(20091003103,21907100,69,10,NULL),
(20091003103,212120281,62,10,NULL),
(20091003103,11706200,77,10,NULL),
(20091003103,21902001,66,10,NULL),
(20091003103,21922400,97,10,NULL),
(20091003103,21919400,88,10,NULL),
(20091003103,21906601,94,10,NULL),
(20091003103,21903202,98,10,NULL),
(20091003103,21919600,62,10,NULL),
(20091003103,21907202,84,10,NULL),
(20091003103,21921002,73,10,NULL),
(20091003103,21915900,72,10,NULL),
(20091003103,21921400,68,9.5,NULL),
(20091003103,21203800,95,10,NULL),
(20091003103,21216501,70,10,NULL),
(20091003103,21904802,69,10,NULL),
(20091003103,21213501,87,10,NULL),
(20091003103,21922700,70,10,NULL),
(20091003103,10900500,70,10,NULL),
(20091003103,21922500,74,10,NULL),
(20091003103,21921303,80,10,NULL),
(20091003103,21921502,96,10,NULL),
(20091003786,21922400,83,10,NULL),
(20091003786,21907100,71,10,NULL),
(20091003786,10900500,84,10,NULL),
(20091003786,21922600,72,10,NULL),
(20091003786,11706200,75,10,NULL),
(20091003786,21921502,78,10,NULL),
(20091003786,21915601,73,10,NULL),
(20091003786,21921400,78,10,NULL),
(20091003786,21922500,65,10,NULL),
(20091003786,21921303,91,10,NULL),
(20091003786,21920300,74,10,NULL),
(20091003786,21919400,76,10,NULL),
(20091003786,21216501,67,10,NULL),
(20091003786,212120281,82,9,NULL),
(20091003786,21919600,81,10,NULL),
(20091003786,21915900,55,10,NULL),
(20091003786,21203800,62,10,NULL),
(20091003786,21922700,84,10,NULL),
(20091003818,10900500,80,10,NULL),
(20091003818,21922400,93,10,NULL),
(20091003818,21921502,92,10,NULL),
(20091003818,21915900,91,9.5,NULL),
(20091003818,21216501,75,10,NULL),
(20091003818,21921400,89,10,NULL),
(20091003818,21907202,88,10,NULL),
(20091003818,21922600,83,10,NULL),
(20091003930,21906601,80,10,NULL),
(20091003930,21907100,73,10,NULL),
(20091003930,21915900,70,10,NULL),
(20091003930,21922500,77,10,NULL),
(20091003930,21921400,68,10,NULL),
(20091003930,21921002,79,10,NULL),
(20091003930,21213501,75,10,NULL),
(20091003930,21919600,59,10,NULL),
(20091003930,21902001,81,9.5,NULL),
(20091003930,21907202,70,10,NULL),
(20091003930,21921303,85,10,NULL),
(20091003930,21904802,80,10,NULL),
(20091003930,11706200,87,10,NULL),
(20091003930,21922600,74,10,NULL),
(20091003930,21922700,82,10,NULL),
(20091003930,21921502,77,10,NULL),
(20081002789,21904802,66,10,NULL),
(20081002789,21922600,80,9.5,NULL),
(20081002789,212120281,74,10,NULL),
(20081002789,21922400,74,10,NULL),
(20081002789,21921502,75,10,NULL),
(20081002789,21915601,63,10,NULL),
(20081002789,21907202,75,10,NULL),
(20081002789,21920300,74,10,NULL),
(20081002789,21203800,78,10,NULL),
(20081002789,21919400,65,10,NULL),
(20081002789,21921002,72,10,NULL),
(20081002789,21903202,83,10,NULL),
(20081002789,21921303,75,10,NULL),
(20081002789,21902001,94,10,NULL),
(20081002789,21922500,72,10,NULL),
(20081002789,21921400,87,10,NULL),
(20091003766,21922400,74,10,NULL),
(20091003766,21921400,79,10,NULL),
(20091003766,21922600,96,10,NULL),
(20091003766,21919400,89,10,NULL),
(20091003766,21902001,62,10,NULL),
(20091003766,21922700,65,10,NULL),
(20091003766,21922500,64,10,NULL),
(20091003766,21921502,78,10,NULL),
(20091003766,21903202,88,10,NULL),
(20091003766,212120281,65,10,NULL),
(20091003766,21907100,67,10,NULL),
(20091003766,10900500,81,10,NULL),
(20091003766,21904802,64,10,NULL),
(20091003766,21920300,72,10,NULL),
(20091003766,21907202,65,10,NULL),
(20091003766,21216501,74,9.5,NULL),
(20091003766,21203800,64,10,NULL),
(20091003766,21915601,85,10,NULL),
(20091003766,21213501,73,10,NULL),
(20091003766,21906601,76,10,NULL),
(20081003494,21213501,82,10,NULL),
(20081003494,21904802,58,10,NULL),
(20081003494,21922400,71,10,NULL),
(20081003494,21920300,85,10,NULL),
(20081003494,10900500,77,10,NULL),
(20081003494,21915601,72,10,NULL),
(20081003494,21922600,80,10,NULL),
(20081003494,21921002,40,9.5,NULL),
(20081003494,21922700,74,10,NULL),
(20081003494,21921400,89,10,NULL),
(20081003494,212120281,69,10,NULL),
(20081003494,21919400,86,9,NULL),
(20081003494,21203800,74,10,NULL),
(20081003494,21216501,70,10,NULL),
(20081003494,21902001,71,10,NULL),
(20081003494,21919600,73,10,NULL),
(20081003494,21907100,62,10,NULL),
(20081003494,21906601,79,10,NULL),
(20081003494,21903202,82,10,NULL),
(20081003494,21921502,91,9.5,NULL),
(20081003494,21921303,86,10,NULL),
(20081003494,21915900,86,10,NULL);
", DB.con);
            cmd.ExecuteNonQuery();
        }
    }
}