package com.flotix;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.oauth2.config.annotation.configurers.ClientDetailsServiceConfigurer;
import org.springframework.security.oauth2.config.annotation.web.configuration.AuthorizationServerConfigurerAdapter;
import org.springframework.security.oauth2.config.annotation.web.configuration.EnableAuthorizationServer;
import org.springframework.security.oauth2.config.annotation.web.configurers.AuthorizationServerEndpointsConfigurer;
import org.springframework.security.oauth2.provider.token.TokenStore;
import org.springframework.security.oauth2.provider.token.store.InMemoryTokenStore;

/**
 * Configuracion de la autorizacion para el acceso a la aplicacion
 * 
 * @author Flor
 *
 */
@Configuration
@EnableAuthorizationServer
public class AuthorizacionServerConfiguration extends AuthorizationServerConfigurerAdapter {

	@Value("${security.oauth.client_id}")
	private String client_id;

	@Value("${security.oauth.secret}")
	private String secret;

	@Value("${security.oauth.grant_type}")
	private String grant_type_password;

	@Value("${security.oauth.grant_type.authorization_code}")
	private String grant_type_authorization_code;

	@Value("${security.oauth.grant_type.refresh_token}")
	private String grant_type_refresh_token;

	@Value("${security.oauth.grant_type.implicit}")
	private String grant_type_implicit;

	@Value("${security.oauth.authorities.role_client}")
	private String authorities_role_client;

	@Value("${security.oauth.authorities.role_trusted_client}")
	private String authorities_role_trusted_client;

	@Value("${security.oauth.scope_read}")
	private String scope_read;

	@Value("${security.oauth.scope_write}")
	private String scope_write;

	@Value("${security.oauth.access_token_validity}")
	private int access_token_validity;

	@Autowired
	@Qualifier("authenticationManagerBean")
	private AuthenticationManager authenticationManager;

	@Autowired
	private TokenStore tokenStore;

	/**
	 * Configura la autenticacion
	 */
	@Override
	public void configure(ClientDetailsServiceConfigurer clients) throws Exception {
		clients.inMemory().withClient(client_id)
				.authorizedGrantTypes(grant_type_password, grant_type_authorization_code, grant_type_refresh_token,
						grant_type_implicit)
				.authorities(authorities_role_client, authorities_role_trusted_client).scopes(scope_read, scope_write)
				.autoApprove(true).secret(passwordEncoder().encode(secret))
				.accessTokenValiditySeconds(access_token_validity);
	}

	@Bean
	public PasswordEncoder passwordEncoder() {
		return new BCryptPasswordEncoder();
	}

	@Override
	public void configure(AuthorizationServerEndpointsConfigurer endpoints) throws Exception {
		endpoints.authenticationManager(authenticationManager).tokenStore(tokenStore);
	}

	@Bean
	public TokenStore tokenStore() {
		return new InMemoryTokenStore();
	}
}