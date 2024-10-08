<?php
/**
 * Copyright since 2007 PrestaShop SA and Contributors
 * PrestaShop is an International Registered Trademark & Property of PrestaShop SA
 *
 * NOTICE OF LICENSE
 *
 * This source file is subject to the Academic Free License version 3.0
 * that is bundled with this package in the file LICENSE.md.
 * It is also available through the world-wide-web at this URL:
 * https://opensource.org/licenses/AFL-3.0
 * If you did not receive a copy of the license and are unable to
 * obtain it through the world-wide-web, please send an email
 * to license@prestashop.com so we can send you a copy immediately.
 *
 * @author    PrestaShop SA and Contributors <contact@prestashop.com>
 * @copyright Since 2007 PrestaShop SA and Contributors
 * @license   https://opensource.org/licenses/AFL-3.0 Academic Free License version 3.0
 */

 declare(strict_types=1);

if (!defined('_PS_VERSION_')) {
    exit;
}

class alternetisprestaconnect extends Module
{
    public function __construct()
    {
        $this->name = 'alternetisprestaconnect';
        $this->tab = 'Back_Office_feature';
        $this->version = '1.0.0';
        $this->author = 'Alternetis';
        $this->need_instance = 0;
        $this->ps_versions_compliancy = [
            'min' => '1.7',
            'max' => _PS_VERSION_
        ];

        parent::__construct();

        $this->displayName = $this->l('Module Prestaconnect');
        $this->description = $this->l('Module permettant de connecter Prestaconnect à prestashop');

        $this->confirmUninstall = $this->l('Êtes-vous sûr de vouloir désinstaller ce module ?');

       if (!Configuration::get('alternetis_prestaconnect_PAGENAME')) 
        {
            $this->warning = $this->l('Aucun nom fourni');
        }
 
    }
    
    public function install()
    {
        if (Shop::isFeatureActive())
            {
                Shop::setContext(Shop::CONTEXT_ALL);
            }
        
        if (!parent::install() ||
            !$this->registerHook('leftColumn') ||
            !Configuration::updateValue('alternetis_prestaconnect_PAGENAME', 'Mentions légales')
        ) 
        {
            return false;
        }
    
        return true;
    }

    public function uninstall()
    {
        if (!parent::uninstall() ||
            !Configuration::deleteByName('NS_MONMODULE_PAGENAME')
        ) {
            return false;
        }
    
        return true;
    }

}

