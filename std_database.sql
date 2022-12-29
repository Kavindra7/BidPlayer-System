/*
 Navicat Premium Data Transfer

 Source Server         : XAMPP
 Source Server Type    : MySQL
 Source Server Version : 100424 (10.4.24-MariaDB)
 Source Host           : localhost:3306
 Source Schema         : std_database

 Target Server Type    : MySQL
 Target Server Version : 100424 (10.4.24-MariaDB)
 File Encoding         : 65001

 Date: 11/10/2022 20:50:39
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for bid_players
-- ----------------------------
DROP TABLE IF EXISTS `bid_players`;
CREATE TABLE `bid_players`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `owner_id` int NULL DEFAULT NULL,
  `player_id` int NULL DEFAULT NULL,
  `thophy_id` int NULL DEFAULT NULL,
  `bid_price` decimal(10, 2) NULL DEFAULT NULL,
  `status` enum('Sold','Unsold','Pending') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT current_timestamp ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 15 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of bid_players
-- ----------------------------
INSERT INTO `bid_players` VALUES (1, 1, 2, 2, 8000.00, 'Sold', '2022-10-07 02:43:27');
INSERT INTO `bid_players` VALUES (14, 3, 2, 2, 10000.00, 'Sold', '2022-10-07 02:55:52');

-- ----------------------------
-- Table structure for my_players
-- ----------------------------
DROP TABLE IF EXISTS `my_players`;
CREATE TABLE `my_players`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `player_name` int NULL DEFAULT NULL,
  `bidder_id` int NULL DEFAULT NULL,
  `bid_price` decimal(10, 2) NULL DEFAULT NULL,
  `status` enum('Won','Taken','Pending') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of my_players
-- ----------------------------
INSERT INTO `my_players` VALUES (1, 2, 1, 20000.00, 'Pending');
INSERT INTO `my_players` VALUES (2, 2, 3, 10000.00, 'Won');

-- ----------------------------
-- Table structure for permissions
-- ----------------------------
DROP TABLE IF EXISTS `permissions`;
CREATE TABLE `permissions`  (
  `permission_id` int NOT NULL AUTO_INCREMENT,
  `permission` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `role_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`permission_id`) USING BTREE,
  INDEX `role_id`(`role_id` ASC) USING BTREE,
  CONSTRAINT `permissions_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `roles` (`role_id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 375 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of permissions
-- ----------------------------
INSERT INTO `permissions` VALUES (288, 'home/list', 10);
INSERT INTO `permissions` VALUES (289, 'bid_players/list', 10);
INSERT INTO `permissions` VALUES (290, 'bid_players/view', 10);
INSERT INTO `permissions` VALUES (291, 'bid_players/add', 10);
INSERT INTO `permissions` VALUES (292, 'bid_players/edit', 10);
INSERT INTO `permissions` VALUES (293, 'bid_players/delete', 10);
INSERT INTO `permissions` VALUES (294, 'bid_players/importdata', 10);
INSERT INTO `permissions` VALUES (295, 'my_players/list', 10);
INSERT INTO `permissions` VALUES (296, 'my_players/view', 10);
INSERT INTO `permissions` VALUES (297, 'my_players/add', 10);
INSERT INTO `permissions` VALUES (298, 'my_players/edit', 10);
INSERT INTO `permissions` VALUES (299, 'my_players/delete', 10);
INSERT INTO `permissions` VALUES (300, 'my_players/importdata', 10);
INSERT INTO `permissions` VALUES (301, 'teams/list', 10);
INSERT INTO `permissions` VALUES (302, 'teams/view', 10);
INSERT INTO `permissions` VALUES (303, 'teams/add', 10);
INSERT INTO `permissions` VALUES (304, 'teams/edit', 10);
INSERT INTO `permissions` VALUES (305, 'teams/delete', 10);
INSERT INTO `permissions` VALUES (306, 'teams/importdata', 10);
INSERT INTO `permissions` VALUES (307, 'trophies/list', 10);
INSERT INTO `permissions` VALUES (308, 'trophies/view', 10);
INSERT INTO `permissions` VALUES (309, 'trophies/add', 10);
INSERT INTO `permissions` VALUES (310, 'trophies/edit', 10);
INSERT INTO `permissions` VALUES (311, 'trophies/delete', 10);
INSERT INTO `permissions` VALUES (312, 'trophies/importdata', 10);
INSERT INTO `permissions` VALUES (313, 'users/list', 10);
INSERT INTO `permissions` VALUES (314, 'users/view', 10);
INSERT INTO `permissions` VALUES (315, 'users/add', 10);
INSERT INTO `permissions` VALUES (316, 'users/edit', 10);
INSERT INTO `permissions` VALUES (317, 'users/delete', 10);
INSERT INTO `permissions` VALUES (318, 'users/importdata', 10);
INSERT INTO `permissions` VALUES (319, 'account/list', 10);
INSERT INTO `permissions` VALUES (320, 'account/edit', 10);
INSERT INTO `permissions` VALUES (321, 'roles/list', 10);
INSERT INTO `permissions` VALUES (322, 'roles/view', 10);
INSERT INTO `permissions` VALUES (323, 'roles/add', 10);
INSERT INTO `permissions` VALUES (324, 'roles/edit', 10);
INSERT INTO `permissions` VALUES (325, 'roles/delete', 10);
INSERT INTO `permissions` VALUES (326, 'permissions/list', 10);
INSERT INTO `permissions` VALUES (327, 'permissions/view', 10);
INSERT INTO `permissions` VALUES (328, 'permissions/add', 10);
INSERT INTO `permissions` VALUES (329, 'permissions/edit', 10);
INSERT INTO `permissions` VALUES (330, 'permissions/delete', 10);
INSERT INTO `permissions` VALUES (331, 'register_with_trophy/list', 10);
INSERT INTO `permissions` VALUES (332, 'register_with_trophy/view', 10);
INSERT INTO `permissions` VALUES (333, 'register_with_trophy/add', 10);
INSERT INTO `permissions` VALUES (334, 'register_with_trophy/edit', 10);
INSERT INTO `permissions` VALUES (335, 'register_with_trophy/delete', 10);
INSERT INTO `permissions` VALUES (336, 'home/list', 11);
INSERT INTO `permissions` VALUES (337, 'bid_players/list', 11);
INSERT INTO `permissions` VALUES (338, 'bid_players/view', 11);
INSERT INTO `permissions` VALUES (339, 'teams/view', 11);
INSERT INTO `permissions` VALUES (340, 'trophies/list', 11);
INSERT INTO `permissions` VALUES (341, 'trophies/view', 11);
INSERT INTO `permissions` VALUES (342, 'users/view', 11);
INSERT INTO `permissions` VALUES (343, 'account/list', 11);
INSERT INTO `permissions` VALUES (344, 'account/edit', 11);
INSERT INTO `permissions` VALUES (345, 'register_with_trophy/list', 11);
INSERT INTO `permissions` VALUES (346, 'register_with_trophy/view', 11);
INSERT INTO `permissions` VALUES (347, 'register_with_trophy/add', 11);
INSERT INTO `permissions` VALUES (348, 'register_with_trophy/edit', 11);
INSERT INTO `permissions` VALUES (349, 'register_with_trophy/delete', 11);
INSERT INTO `permissions` VALUES (350, 'home/list', 12);
INSERT INTO `permissions` VALUES (351, 'bid_players/list', 12);
INSERT INTO `permissions` VALUES (352, 'bid_players/view', 12);
INSERT INTO `permissions` VALUES (353, 'bid_players/add', 12);
INSERT INTO `permissions` VALUES (354, 'bid_players/edit', 12);
INSERT INTO `permissions` VALUES (355, 'bid_players/delete', 12);
INSERT INTO `permissions` VALUES (356, 'bid_players/importdata', 12);
INSERT INTO `permissions` VALUES (357, 'my_players/list', 12);
INSERT INTO `permissions` VALUES (358, 'my_players/view', 12);
INSERT INTO `permissions` VALUES (359, 'my_players/add', 12);
INSERT INTO `permissions` VALUES (360, 'my_players/edit', 12);
INSERT INTO `permissions` VALUES (361, 'my_players/delete', 12);
INSERT INTO `permissions` VALUES (362, 'my_players/importdata', 12);
INSERT INTO `permissions` VALUES (363, 'teams/view', 12);
INSERT INTO `permissions` VALUES (364, 'trophies/list', 12);
INSERT INTO `permissions` VALUES (365, 'trophies/view', 12);
INSERT INTO `permissions` VALUES (367, 'trophies/edit', 12);
INSERT INTO `permissions` VALUES (368, 'trophies/delete', 12);
INSERT INTO `permissions` VALUES (369, 'trophies/importdata', 12);
INSERT INTO `permissions` VALUES (370, 'users/view', 12);
INSERT INTO `permissions` VALUES (371, 'account/list', 12);
INSERT INTO `permissions` VALUES (372, 'account/edit', 12);
INSERT INTO `permissions` VALUES (373, 'register_with_trophy/list', 12);
INSERT INTO `permissions` VALUES (374, 'register_with_trophy/view', 12);

-- ----------------------------
-- Table structure for register_with_trophy
-- ----------------------------
DROP TABLE IF EXISTS `register_with_trophy`;
CREATE TABLE `register_with_trophy`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `trophy_id` int NULL DEFAULT NULL,
  `player_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of register_with_trophy
-- ----------------------------
INSERT INTO `register_with_trophy` VALUES (1, 2, 1);
INSERT INTO `register_with_trophy` VALUES (2, 2, 2);

-- ----------------------------
-- Table structure for roles
-- ----------------------------
DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles`  (
  `role_id` int NOT NULL AUTO_INCREMENT,
  `role_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`role_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of roles
-- ----------------------------
INSERT INTO `roles` VALUES (10, 'Admin');
INSERT INTO `roles` VALUES (11, 'Player');
INSERT INTO `roles` VALUES (12, 'Team Owner');

-- ----------------------------
-- Table structure for teams
-- ----------------------------
DROP TABLE IF EXISTS `teams`;
CREATE TABLE `teams`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `team_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `max_players` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `max_price` decimal(10, 2) NULL DEFAULT NULL,
  `status` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `owner_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of teams
-- ----------------------------
INSERT INTO `teams` VALUES (1, 'Team A', '12', 20000.00, 'Active', 3);
INSERT INTO `teams` VALUES (2, 'Team 2', '12', 40000.00, 'Active', 3);

-- ----------------------------
-- Table structure for trophies
-- ----------------------------
DROP TABLE IF EXISTS `trophies`;
CREATE TABLE `trophies`  (
  `trophy_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `team_1` int NULL DEFAULT NULL,
  `team_2` int NULL DEFAULT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  `prize` double(15, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `team_leader_id`(`team_2` ASC) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of trophies
-- ----------------------------
INSERT INTO `trophies` VALUES ('World Cup', 1, 1, 2, 50000.00);

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `full_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `username` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `age` int NULL DEFAULT NULL,
  `gender` enum('Male','Female') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `balance` double(15, 2) NULL DEFAULT NULL,
  `user_role_id` int NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `user_role_id`(`user_role_id` ASC) USING BTREE,
  CONSTRAINT `users_ibfk_1` FOREIGN KEY (`user_role_id`) REFERENCES `roles` (`role_id`) ON DELETE SET NULL ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (1, 'Administrator', 'Admin', 'admin@esoft.lk', 'kCSimZWEYTCX26n7ddRKlg==:C8KHU4HDnh4mJf19FKzcXg==', 22, 'Male', 2000000.00, 10);
INSERT INTO `users` VALUES (2, 'Player 1', 'Player1', 'player1@esoft.lk', 'WuvkPlgFyeZUX+nJl2ACDg==:ktahXrUqO5+o2euJLMZHkw==', 34, 'Male', 20000.00, 11);
INSERT INTO `users` VALUES (3, 'Team Owner 1', 'Team1', 'team1@esoft.lk', '+NFbDMtKxof2L9lMDl9dDA==:S7lQLQ4JRt/DBjFeboyLZg==', 43, 'Male', 50000.00, 12);

SET FOREIGN_KEY_CHECKS = 1;
