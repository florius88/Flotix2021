package com.flotix.utils;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class UtilEncryptor {

	public static void main(String[] args) {

		String ver = convertirSHA256("elias2");
		System.out.println(ver);

	}

	/**
	 * Convierte de string a byteArray
	 */
	public static String convertirSHA256(String password) {
		MessageDigest md = null;
		try {
			md = MessageDigest.getInstance("SHA-256");
		} catch (NoSuchAlgorithmException e) {
			e.printStackTrace();
			return null;
		}

		byte[] hash = md.digest(password.getBytes());
		StringBuffer sb = new StringBuffer();

		for (byte b : hash) {
			sb.append(String.format("%02x", b));
		}

		return sb.toString();
	}

	/*
	 * fun encrypt(pwd: String): String? { var md: MessageDigest? var bytes:
	 * ByteArray? = null try { md = MessageDigest.getInstance("SHA-256") bytes =
	 * md.digest(pwd.toByteArray(charset("UTF-8"))) } catch (ex: Exception) { }
	 * return convertToHex(bytes) }
	 */

	/**
	 * Convierte un ByteArray en un String
	 */
	/*
	 * fun convertToHex(bytes: ByteArray?): String? { val sb = StringBuffer() for (i
	 * in bytes!!.indices) { sb.append(((bytes[i] and 0xff.toByte()) +
	 * 0x100).toString(16).substring(1)) } return sb.toString() }
	 */

}
